using UnityEngine;
using UnityEngine.UI;

public class SlingShot : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private LineRenderer[] strips;
    [SerializeField] private Transform center;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform[] stripPositions;
    [SerializeField] private float maxLength;
    [SerializeField] private float potionPositionOffset;
    [SerializeField] private int forceMultiplier;
    [SerializeField] private Transform pointsParent;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private int numberOfPoints;

    private GameObject _potion;
    private Rigidbody2D rbPotion;
    private Collider2D potionCollider;
    private Camera _camera;
    private float force;
    private Vector3 currentPosition;
    private Vector2 direction;
    private bool isMouseDown;
    private GameObject[] points;


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

        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, center.position, Quaternion.identity, pointsParent);
        }

        HidePoints();
    }

    private void Update()
    {
        if (isMouseDown)
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            currentPosition = center.position - Vector3.ClampMagnitude(center.position - mousePosition, maxLength);
            SetStrips(currentPosition);
            direction = (Vector2)(center.position - currentPosition);
            force = direction.magnitude * forceMultiplier;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].transform.position = GetPointPosition(i * .1f);
            }

            ShowPoints();

            if (potionCollider)
            {
                potionCollider.enabled = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (_potion == null) return;
        
        isMouseDown = true;
        AudioSystem.Instance.Play("Agarrar Tirachinas");
    }

    private void OnMouseUp()
    {
        if (_potion == null) return;
        
        isMouseDown = false;
        Shoot();
        ResetStrips();
        HidePoints();
    }

    private void Shoot()
    {
        AudioSystem.Instance.Play("Lanzar Pocion");
        rbPotion.isKinematic = false;
        Vector3 potionForce = direction.normalized * force * 1.5f;
        rbPotion.velocity = potionForce;
        
        rbPotion = null;
        potionCollider = null;
        _potion = null;

        _levelManager.HasPotion = false;
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
        _levelManager.HasPotion = true;
        _potion = potion;
        rbPotion = _potion.GetComponent<Rigidbody2D>();
        potionCollider = _potion.GetComponent<Collider2D>();
        potionCollider.enabled = false;
    }

    private Vector2 GetPointPosition(float time)
    {
        Vector2 currentPosition = (Vector2)center.position + (direction.normalized * force * time) + .5f * Physics2D.gravity * (time * time);
        return currentPosition;
    }

    private void ShowPoints()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<Image>().enabled = true;
        }
    }

    private void HidePoints()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<Image>().enabled = false;
        }
    }
}
