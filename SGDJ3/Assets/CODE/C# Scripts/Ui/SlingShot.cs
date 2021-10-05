using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField] private LineRenderer[] strips;
    [SerializeField] private Transform center;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform[] stripPositions;
    [SerializeField] private float maxLength;
    [SerializeField] private float potionPositionOffset;

    private GameObject _potion;
    private Rigidbody2D rbPotion;
    private Collider2D potionCollider;
    private Camera _camera;
    private Vector3 currentPosition;
    private bool isMouseDown;

    private void Start()
    {
        _camera = Camera.main;
        
        // por un problema de conversión entre RectTransform y Transform hay que poner a 0
        // el eje Z en la posicion
        center.position = new Vector3(center.position.x, center.position.y, 0f);
        idlePosition.position = new Vector3(idlePosition.position.x, idlePosition.position.y, 0f);
        stripPositions[0].position = new Vector3(stripPositions[0].position.x, stripPositions[0].position.y, 0f);
        stripPositions[1].position = new Vector3(stripPositions[1].position.x, stripPositions[1].position.y, 0f);
        
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
            currentPosition = center.position - Vector3.ClampMagnitude(center.position - mousePosition, maxLength);
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

        if (_potion != null)
        {
            if (isMouseDown)
            {
                Vector3 direction = position - center.position;
                _potion.transform.position = position + direction.normalized * potionPositionOffset;    
            }
            else
            {
                _potion.transform.position = center.position;
            }
            
        }
    }

    public void SetPotion(GameObject potion)
    {
        _potion = potion;
        rbPotion = _potion.GetComponent<Rigidbody2D>();
        potionCollider = _potion.GetComponent<Collider2D>();
        potionCollider.enabled = false;
    }
}
