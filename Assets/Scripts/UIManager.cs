using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Tiles tiles;
    private Sorter sorter;

    public Transform RealObjParent;
    public Transform InteractableBtn;

    public SpriteRenderer grids;

    public GameObject RoomSettingPanel;
    public GameObject ReturnPanel;
    public GameObject Room;
    private GameObject targetObj;
    private FurnitureObj SelectedFurniture;

    public bool dragging = false;

    public Button returnBtn;
    public Button checkBtn;
    public Toggle mode;

    private GameObject[] RealObjList= null;
    private int arrSize = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        sorter = GameObject.Find("object").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();
    }
    void Start()
    {
        mode.onValueChanged.AddListener(value => RoomSettingPanel.SetActive(value));
        mode.onValueChanged.AddListener(value => ReturnPanel.SetActive(value));
        mode.onValueChanged.AddListener(value => grids.enabled = value);

        arrSize = RealObjParent.transform.childCount;
        RealObjList= new GameObject[arrSize];
        for(int i = 0; i < arrSize;i++)
        {
            RealObjList[i] = RealObjParent.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (!mode.isOn)
            return;
        mode.interactable = SelectedFurniture == null; //변수가 null일 때 interactable에 false를 저장

        if (Input.GetMouseButtonDown(0))
            OnBeginDrag(isHold => dragging = isHold); //즉, isHold 값이 변경될 때마다 dragging 변수에도 해당 값을 할당하여 상태를 유지한다.

        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            OnEndDrag();
        }

        if (dragging)
            OnDrag();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void OnBeginDrag(Action<bool> isHold)
    {

        if (SelectedFurniture == null)
        {
            var furniture = OnSelect(obj => obj.transform.GetComponent<FurnitureObj>() != null);
            if (furniture != null)
            {
                SelectedFurniture = furniture.transform.GetComponent<FurnitureObj>();
                SelectedFurniture.Unplaced();
            }
            isHold(furniture != null);
        }
        else
        {
            var furniture = OnSelect(child => child.transform.GetComponent<FurnitureObj>() != null);
            isHold(furniture != null && furniture.transform.GetComponent<FurnitureObj>() == SelectedFurniture);
        }

    }

    private void OnDrag()
    {
        if (SelectedFurniture == null)
            return;

        var tile = OnSelectTile(obj => obj.GetComponent<Tile>() != null);
        if (tile != null)
        {
            InteractableBtn.gameObject.SetActive(false);
            SelectedFurniture.Move(tile.GetComponent<Tile>());

            List<Tile> area;
            OnInvalid(SelectedFurniture, out area);
        }
    }
    private void OnEndDrag()
    {
        if (SelectedFurniture == null)
            return;

        InteractableBtn.position = new Vector3(SelectedFurniture.transform.position.x, SelectedFurniture.transform.position.y - 6, 0);
        InteractableBtn.gameObject.SetActive(true);

        List<Tile> area;
        checkBtn.interactable = !(OnInvalid(SelectedFurniture, out area));
        returnBtn.interactable = SelectedFurniture.previous != null;
    }

    private GameObject OnSelect(Predicate<GameObject> condition)
    {
        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D newRay = new Ray2D(ray, Vector2.zero);
        var hits = Physics2D.RaycastAll(newRay.origin, newRay.direction);
        foreach (var hit in hits)
        {
            if (condition(hit.transform.gameObject))
                return hit.transform.gameObject;
        }

        return null;
    }

    private GameObject OnSelectTile(Predicate<GameObject> condition)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, Mathf.Infinity);
        
        foreach(var hit in hits)
        {
            if (condition(hit.transform.gameObject))
                return hit.transform.gameObject;
        }
        return null;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!mode.isOn) mode.enabled = true;
        else mode.enabled = false;
    }

    public void CancelBtn()
    {
        RoomSettingPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        mode.isOn = false;

        sorter.SortAll();
        InteractableBtn.gameObject.SetActive(false);
        targetObj.SetActive(false);
    }

    public void PlaceBtn()
    {
        onPlaceFurniture(SelectedFurniture);
        sorter.SortAll();

        InteractableBtn.gameObject.SetActive(false);
        RoomSettingPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        mode.interactable = true;
        mode.isOn = false;

        targetObj = null;
    }

    public void UndoBtn()
    {
        OnUndo(SelectedFurniture);
        InteractableBtn.gameObject.SetActive(false);
        RoomSettingPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        mode.isOn = false;
    }

    public void onClickObj()
    {
        GameObject currentClick = EventSystem.current.currentSelectedGameObject;

        for(int i = 0;i < RealObjList.Length;i++)
        {
            if(currentClick.name == RealObjList[i].name)
            {
                targetObj = RealObjList[i];
                targetObj.transform.position = new Vector3(Room.transform.position.x, Room.transform.position.y, -1);
                targetObj.SetActive(true);

                InteractableBtn.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 5.3f, 0);
                InteractableBtn.gameObject.SetActive(true);
            }
        }
    }

    private void onPlaceFurniture(FurnitureObj furniture)
    {
        if(furniture == null) return;

        List<Tile> area;
        if(!OnInvalid(furniture, out area))
        {
            furniture.Place(area);
            furniture.SetColor(Color.white);
            SelectedFurniture = null;
        }

    }

    private bool OnInvalid(FurnitureObj furniture, out List<Tile> area)
    {
        area = new List<Tile>();
        for (int i = 0; i < furniture.width; i++)
        {
            for (int j = 0; j < furniture.length; j++)
            {
                var tile = tiles.GetTileByCoordinate(furniture.origin.x + j, furniture.origin.y + i);
                if (tile == null || tile.isBlock)
                {
                    Debug.Log(tile);
                    furniture.SetColor(Color.red);
                    return true;
                }

                area.Add(tile);
            }
        }

        furniture.SetColor(Color.green);
        return false;
    }

    private void OnUndo(FurnitureObj furniture)
    {
        if (furniture.previous == null)
            return;

        furniture.Move(furniture.previous.tile);
        furniture.gameObject.SetActive(false);
    }
}
