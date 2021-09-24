using UnityEngine;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    [SerializeField] private Transform potionSpawnPoint;
    [SerializeField] private Transform witchSprite;
    [SerializeField] private GameObject poof_prefab;
    [SerializeField] private GameObject smoke_prefab;

    public Recipe MixIngredients(string ingredient1, string ingredient2)
    {
        if (ingredient1.Equals( ingredient2 ))
        {
            return  OneIngredientMix(ingredient1);           
        }

        foreach (var recipe in recipes)
        {
            if (recipe.ingredients.Contains(ingredient1) && recipe.ingredients.Contains(ingredient2))
            {
                return recipe;
            }
        }
        return null;
    }

    private Recipe OneIngredientMix(string ingredient)
    {
        if (ingredient.Equals("mush"))
        {
            foreach (var recipe in recipes)
            {
                if (recipe.id.Equals("PotionPink"))
                {
                    return recipe;
                }
            }
            Debug.LogError("No está la pocion rosa en la lista de recetas!?");
            return null;
        }
        else return null;// porque no hay otra pocion de 2 ingredientes buena.
    }
    public void SpawnPotion(Recipe potion, Ingredient ingredient1, Ingredient ingredient2)
    {
        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);

        // aqui se podria instanciar el efecto de crafteando y que la pocion esperase a que terminara para instanciarse
        var poof1 = Instantiate(poof_prefab, ingredient2.transform.position, Quaternion.identity);
        Destroy(poof1, 2);
        var poof2 = Instantiate(poof_prefab, potionSpawnPoint.position, Quaternion.identity);
        Destroy(poof2, 2);
        Instantiate(potion, potionSpawnPoint.position, Quaternion.identity);
    }

    public void WrongMix(Ingredient ingredient1, Ingredient ingredient2)
    {
        var poof1 = Instantiate(poof_prefab, ingredient2.transform.position, Quaternion.identity);
        Destroy(poof1, 2);
        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);

        var smoke = Instantiate(smoke_prefab, witchSprite.transform.position, Quaternion.identity);
        Destroy(smoke, 4);

        GameObject.FindObjectOfType<LevelManager>().Damaged();
    }
}

