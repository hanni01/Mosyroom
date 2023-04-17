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

    void OnMouseDrag()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().InteractableBtn.gameObject.SetActive(false);

        transform.parent.gameObject.GetComponent<objectSet>().isDragObj = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().InteractableBtn.position = 
            new Vector3(transform.position.x, transform.position.y - 1.3f, 0);
        GameObject.Find("UIManager").GetComponent<UIManager>().InteractableBtn.gameObject.SetActive(true);

        transform.parent.gameObject.GetComponent<objectSet>().isDragObj = false;
    }
}
