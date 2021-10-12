using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Collider2D _collider;
    private string firstIngredient;
    private string secondIngredient;


    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        firstIngredient = String.Empty;
        secondIngredient = String.Empty;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
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
