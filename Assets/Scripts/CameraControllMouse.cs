using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllMouse : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;

    public Camera camera_;
    public float dragSpeed;
    private Vector3 prevPos;
    private RaycastHit hit;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư ������ isDragging = true
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("BackGround"))
                {
                    Debug.Log("BackGround ��ġ");
                    dragOrigin = Input.mousePosition;
                    prevPos = transform.position;
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) //���� �巡�� ���
        {
            isDragging = false;

        }

        if (isDragging) //�巡�� ���̶��
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log("�ƹ���");
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
            Vector3 delta = Input.mousePosition - dragOrigin;

            Vector3 newPos = prevPos + move;
            camera_.transform.position = Vector3.Lerp(camera_.transform.position, newPos, Time.deltaTime * 10);
        }
    }
}
