using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllMouse : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;

    public float dragSpeed = 2f;
    private float dragTime = 0f;
    private float dragDistance = 0f;
    private Vector3 prevPos;
    private float dragThreshold = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼 눌리면 isDragging = true
        {
            dragOrigin = Input.mousePosition;
            prevPos = transform.position;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) //떼면 드래그 취소
        {
            isDragging = false;
        }

        if (isDragging) //드래그 중이라면
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
            Vector3 delta = Input.mousePosition - dragOrigin;
            dragTime += Time.deltaTime;
            dragDistance += delta.magnitude; 

            float distance = Vector3.Distance(prevPos, transform.position);
            if (distance >= dragThreshold)
            {
                Vector3 newPos = prevPos + move;
                newPos.x = Mathf.Clamp(newPos.x, -10f, 10f);
                newPos.y = Mathf.Clamp(newPos.y, -10f, 10f);
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);
            }

            float speed = dragDistance / dragTime;
            if (speed > dragSpeed)
            {
                float ratio = dragSpeed / speed;
                dragDistance *= ratio;
                dragTime *= ratio;
            }
        }
    }
}
