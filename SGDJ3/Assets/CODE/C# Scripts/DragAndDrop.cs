using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Transform myTransform;
    private Camera cam;
    
    private void Awake()
    {
        myTransform = transform;
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseDrag()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        myTransform.position = new Vector3(mousePosition.x, mousePosition.y, myTransform.position.z);            
    }

    private void OnMouseUp()
    {
        
    }
}
