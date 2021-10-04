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
    [SerializeField] private float maxLength;

    private Camera _camera;
    private Vector3 currentPosition;
    private bool isMouseDown;

    private void Start()
    {
        _camera = Camera.main;
        strips[0].positionCount = 2;
        strips[1].positionCount = 2;
        strips[0].SetPosition(0, stripPositions[0].position);
        strips[1].SetPosition(0, stripPositions[1].position);
        ResetStrips();
    }

    private void Update()
    {
        if (isMouseDown)
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            currentPosition = mousePosition;
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            SetStrips(currentPosition);
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        ResetStrips();
    }

    private void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    private void SetStrips(Vector3 position)
    {
        strips[0].SetPosition(1, position);
        strips[1].SetPosition(1, position);
    }
}
