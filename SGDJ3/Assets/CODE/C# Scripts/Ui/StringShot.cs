using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringShot : MonoBehaviour
{
    [SerializeField] private LineRenderer[] strips;
    [SerializeField] private Transform center;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform[] stripPositions;

    private Camera _camera;
    private bool isMouseDown;

    private void Start()
    {
        _camera = Camera.main;
        strips[0].positionCount = 2;
        strips[1].positionCount = 2;
        strips[0].SetPosition(0, stripPositions[0].position);
        strips[1].SetPosition(0, stripPositions[1].position);
    }

    private void Update()
    {
        if (isMouseDown)
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            SetStrips(mousePosition);
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
    }

    private void ResetStrips()
    {
        SetStrips(idlePosition.position);
    }

    private void SetStrips(Vector3 position)
    {
        strips[0].SetPosition(1, position);
        strips[1].SetPosition(1, position);
    }
}
