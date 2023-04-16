using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform RealObjParent;
    public Transform InteractableBtn;

    public SpriteRenderer grids;

    public GameObject RoomSettingPanel;
    public GameObject ReturnPanel;
    public GameObject Room;
    private GameObject targetObj;

    public Button returnBtn;
    public Button checkBtn;
    public Toggle mode;

    private GameObject[] RealObjList= null;
    private int arrSize = 0;

    // Start is called before the first frame update
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
            RealObjList[i].AddComponent<objectSet>();
        }
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

        if (!targetObj) return;
        targetObj.SetActive(false);
        InteractableBtn.gameObject.SetActive(false);
    }

    public void onClickObj()
    {
        GameObject currentClick = EventSystem.current.currentSelectedGameObject;

        Debug.Log(currentClick);

        for(int i = 0;i < RealObjList.Length;i++)
        {
            if(currentClick.name == RealObjList[i].name)
            {
                targetObj = RealObjList[i];
                targetObj.transform.position = Room.transform.position;
                targetObj.SetActive(true);

                InteractableBtn.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 1.3f, 0);
                InteractableBtn.gameObject.SetActive(true);
            }
        }
    }
}
