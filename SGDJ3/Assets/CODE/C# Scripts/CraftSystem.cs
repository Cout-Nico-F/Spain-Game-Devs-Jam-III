using UnityEngine;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    [SerializeField] private Transform potionSpawnPoint;
    

    public Recipe MixIngredients(string ingredient1, string ingredient2)
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

    public void SpawnPotion(Recipe potion, Ingredient ingredient1, Ingredient ingredient2)
    {
        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);
        
        // aqui se podria instanciar el efecto de puff y que la pocion esperase a que terminara para instanciarse
        Instantiate(potion, potionSpawnPoint.position, Quaternion.identity);
    }

    public void WrongMix(Ingredient ingredient1, Ingredient ingredient2)
    {
        GameObject.FindObjectOfType<LevelManager>().Damaged();

        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);
    }
}

