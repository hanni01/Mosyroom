using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class objectSet : MonoBehaviour
{
    private bool isDragging;
    private Vector3 dragOrigin;
    private Vector3 prevPos;
    private float dragSpeed = 5f;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click object");
            isDragging = true;
            dragOrigin = Input.mousePosition;
            prevPos = transform.position;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDragging  = false;
        }

        if(isDragging)
        {
            OnDrag();
        }
    }

    private void OnDrag()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        Vector3 newPos = prevPos + move;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);
    }
}
