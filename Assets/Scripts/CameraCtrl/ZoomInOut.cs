using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    private float Speed = 0.25f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)//터치 시작하면
            {
                prePos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
            }

            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)//드래그 중이면
            {
                nowPos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
                movePos = (Vector3)(prePos - nowPos);
                GetComponent<Camera>().transform.Translate(movePos * Time.deltaTime * Speed);
                prePos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
            }
        }
    }
}
