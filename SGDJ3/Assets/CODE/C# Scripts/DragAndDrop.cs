using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Transform myTransform;
    private Camera cam;
    private Vector3 offset;
    
    private void Awake()
    {
        myTransform = transform;
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = myTransform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        myTransform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, myTransform.position.z);            
    }

    private void OnMouseUp()
    {
        
    }
}
