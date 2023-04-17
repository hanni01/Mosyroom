using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class objectSet : MonoBehaviour
{
    readonly float distance = 10;
    public bool isDragObj = false;
    private GameObject SelectedGameObj;

    private void Start()
    {
        SelectedGameObj= GetComponent<GameObject>();
    }

    void OnMouseDrag()
    {
        isDragObj = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        isDragObj= false;
    }
}
