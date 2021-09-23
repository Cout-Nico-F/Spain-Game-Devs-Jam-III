using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private float maxDistanceBetweenIngredients;
    
    private Transform myTransform;
    private Camera cam;
    private Vector3 offset;
    private Ingredient myIngredient;
    private Ingredient secondIngredient;
    private Renderer _renderer;
    
    
    private void Awake()
    {
        myTransform = transform;
        myIngredient = GetComponent<Ingredient>();
        cam = Camera.main;
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
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
        if (secondIngredient != null)
        {
            var distance = Vector3.Distance(myTransform.position, secondIngredient.transform.position);
            if (distance > maxDistanceBetweenIngredients) return;
            
            var recipe = CraftSystem.Instance.MixIngredients(myIngredient.id, secondIngredient.id);
            if (recipe != null)
            {
                Instantiate(recipe, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.Log("Receta sin efecto");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            secondIngredient = other.GetComponent<Ingredient>();
        }
    }
}
