using UnityEngine;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    [SerializeField] private Transform potionSpawnPoint;
    [SerializeField] private Transform witchSprite;
    [SerializeField] private GameObject poof_prefab;
    [SerializeField] private GameObject smoke_prefab;
    [SerializeField] private GameObject boom_prefab;
    [SerializeField] private Animator craft_anim;
    private LevelManager levelManager;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    public Recipe MixIngredients(string ingredient1, string ingredient2)
    {
        craft_anim.Play("PotCrafting");
        //GameManager.Instance.WaitForSecondsCoroutine(2);

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

        levelManager.HasPotion = true;
    }

    public void WrongMix(Ingredient ingredient1, Ingredient ingredient2)
    {
        var poof1 = Instantiate(poof_prefab, ingredient2.transform.position, Quaternion.identity);
        Destroy(poof1, 2);
        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);

        var explosion = Instantiate(boom_prefab, craft_anim.transform.position, Quaternion.identity);
        Destroy(explosion, 1.1f);
        var smoke = Instantiate(smoke_prefab, witchSprite.transform.position, Quaternion.identity);
        Destroy(smoke, 1.5f);

        GameObject.FindObjectOfType<LevelManager>().Damaged();
    }
}

