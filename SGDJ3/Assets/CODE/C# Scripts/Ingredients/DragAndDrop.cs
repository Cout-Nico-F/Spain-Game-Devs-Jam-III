using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Transform myTransform;
    private Camera cam;
    private Vector3 offset;
    private Renderer _renderer;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        myTransform = transform;
        cam = Camera.main;
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        AudioSystem.Instance.Play("Ingrediente Pinchado");
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = myTransform.position - mousePosition;
        _renderer.sortingOrder = 100;
    }

    private void OnMouseDrag()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        myTransform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, myTransform.position.z);            
    }

    private void OnMouseUp()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    
}
