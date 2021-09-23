using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Transform myTransform;
    private Camera cam;
    private Vector3 offset;
    private string myIngredient;
    private string secondIngredient;
    private Renderer renderer;
    
    private void Awake()
    {
        myTransform = transform;
        myIngredient = GetComponent<Ingredient>().id;
        cam = Camera.main;
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = myTransform.position - mousePosition;
        renderer.sortingOrder = 100;
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
            var recipe = CraftSystem.Instance.MixIngredients(myIngredient, secondIngredient);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            secondIngredient = other.GetComponent<Ingredient>().id;
        }
    }
}
