using UnityEngine;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    

    public Recipe MixIngredients(Ingredient ingredient1, Ingredient ingredient2)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ingredients.Contains(ingredient1) && recipe.ingredients.Contains(ingredient2))
            {
                return recipe;
            }
        }

        return null;
    }
}

