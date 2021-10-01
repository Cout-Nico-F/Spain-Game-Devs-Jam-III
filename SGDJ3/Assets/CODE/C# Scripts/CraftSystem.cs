using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    [SerializeField] private Transform potionSpawnPoint;
    [SerializeField] private Sprite potExplode;
    [SerializeField] private Sprite potIdle;
    [SerializeField] private GameObject poof_prefab;
    [SerializeField] private GameObject boom_prefab;
    [SerializeField] private Animator craft_anim;
    [SerializeField] private Image witchUi;
    private LevelManager levelManager;
    private bool finishAnimation;
    
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
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
        StartCoroutine(MixIngredientsOK(ingredient2.transform.position, potion));
    }

    public void WrongMix(Ingredient ingredient1, Ingredient ingredient2)
    {
        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);
        StartCoroutine((MixWrongIngredients(ingredient2.transform.position)));
    }


    private IEnumerator MixIngredientsOK(Vector3 spawnPosition, Recipe potion)
    {
        //instanciamos el poof encima de los ingredientes
        AudioSystem.Instance.Play("Ingrediente Mezclado");


        var poof1 = Instantiate(poof_prefab, spawnPosition, Quaternion.identity);
        Destroy(poof1, 2);
        
        //esperamos a que acabe la animacion
        finishAnimation = false;
        StartCoroutine(WaitTime(0.2f));
        while (!finishAnimation)
        {
            yield return null;
        }

        //arrancamos la animación de la olla
        craft_anim.SetTrigger("BubbleUp");
        AudioSystem.Instance.Play("Olla Burbujeando");

        //esperamos a que acabe la animacion
        finishAnimation = false;
        StartCoroutine(WaitTime(1f));
        while (!finishAnimation)
        {
            yield return null;
        }
        
        //instanciamos el poof encima de la olla
        var poof2 = Instantiate(poof_prefab, potionSpawnPoint.position, Quaternion.identity);
        Destroy(poof2, 2);

        AudioSystem.Instance.Play("Potion Spawn");
        //instanciamos la pocion
        Instantiate(potion, potionSpawnPoint.position, Quaternion.identity);
        
        levelManager.HasPotion = true;
    }


    private IEnumerator MixWrongIngredients(Vector3 spawnPosition)
    {
        //instanciamos el poof encima de los ingredientes
        AudioSystem.Instance.Play("Ingrediente Mezclado");

        var poof1 = Instantiate(poof_prefab, spawnPosition, Quaternion.identity);
        Destroy(poof1, 2);
        
        //esperamos a que acabe la animacion
        finishAnimation = false;
        StartCoroutine(WaitTime(0.2f));
        while (!finishAnimation)
        {
            yield return null;
        }

        //arrancamos la animación de la olla

        AudioSystem.Instance.Play("Olla Burbujeando");
       
        craft_anim.SetTrigger("BubbleUp");

        //esperamos a que acabe la animacion
        finishAnimation = false;
        StartCoroutine(WaitTime(1f));
        while (!finishAnimation)
        {
            yield return null;
        }

        //instanciamos la explosion

        AudioSystem.Instance.Play("Explosion");

        var explosion = Instantiate(boom_prefab, craft_anim.transform.position, Quaternion.identity);
        Destroy(explosion, 1.1f);
        
        //cambiamos la imagen a la brujita quemada
        witchUi.sprite = potExplode;
        StartCoroutine(WaitTime(1.2f));
        witchUi.sprite = potIdle;
        
        FindObjectOfType<LevelManager>().Damaged();
    }
    
    
    private IEnumerator WaitTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        finishAnimation = true;
    }
}

