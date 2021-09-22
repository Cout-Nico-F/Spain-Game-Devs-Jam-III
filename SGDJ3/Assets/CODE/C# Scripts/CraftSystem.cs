using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] private Recipe[] recipes;
    
    
    public Recipe MixIngredients(Ingrediente ingrediente1, Ingrediente ingrediente2)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ingredientes.Contains(ingrediente1) && recipe.ingredientes.Contains(ingrediente2))
            {
                return recipe;
            }
        }

        return null;
    }
}

