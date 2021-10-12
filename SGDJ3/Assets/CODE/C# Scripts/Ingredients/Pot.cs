using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private string firstIngredient;
    private string secondIngredient;


    private void Start()
    {
        firstIngredient = String.Empty;
        secondIngredient = String.Empty;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            // si el rigidbody del ingrediente no es dinamico
            // significa que todavia no lo ha soltado y no debemos comprobar nada
            if (other.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Dynamic) return;
            
            // aqui debemos instanciar el efecto en la olla del chapoteo
            
            if (firstIngredient.Equals(String.Empty))
            {
                firstIngredient = other.GetComponent<Ingredient>().id;
            }
            else
            {
                secondIngredient = other.GetComponent<Ingredient>().id;
                MixIngredients();
            }
            
            Destroy(other.gameObject);
        }
    }

    private void MixIngredients()
    {
        var recipe = CraftSystem.Instance.MixIngredients(firstIngredient, secondIngredient);
        if (recipe != null)
        {
            CraftSystem.Instance.SpawnPotion(recipe);
        }
        else
        {
            CraftSystem.Instance.WrongMix();
        }
        
        firstIngredient = String.Empty;
        secondIngredient = String.Empty;
    }
}
