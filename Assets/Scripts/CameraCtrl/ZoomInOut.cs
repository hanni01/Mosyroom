using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    private Vector2 _previousTouchPosition1;
    private Vector2 _previousTouchPosition2; //��ġ�ϴ� �հ��� �ΰ� ��ġ
    private Camera _camera;
    private float _minZoom = 1f;             //���� �ּ�
    private float _maxZoom = 10f;            //�ܾƿ� �ִ�
    private float _zoomSpeed = 0.1f;         //���ϴ� �ӵ�
    public float dragSpeed = 2;
    private RaycastHit hit;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        //���� �ܾƿ�
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touchPosition1 = touch1.position;
            Vector2 touchPosition2 = touch2.position;

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began) //phase�� ��ġ�� ���� ���� ��ġ .Began�� ��ġ ������ ���� ��ġ
            {
                _previousTouchPosition1 = touchPosition1;
                _previousTouchPosition2 = touchPosition2;
            }

            float currentDistance = Vector2.Distance(touchPosition1, touchPosition2);
            float previousDistance = Vector2.Distance(_previousTouchPosition1, _previousTouchPosition2);

            float zoomDifference = previousDistance - currentDistance;
            float zoomAmount = zoomDifference * _zoomSpeed * Time.deltaTime;

            float newOrthographicSize = _camera.orthographicSize + zoomAmount;
            newOrthographicSize = Mathf.Clamp(newOrthographicSize, _minZoom, _maxZoom); //���� ����

            _camera.orthographicSize = newOrthographicSize;

            _previousTouchPosition1 = touchPosition1;
            _previousTouchPosition2 = touchPosition2;
        }
    }
}
