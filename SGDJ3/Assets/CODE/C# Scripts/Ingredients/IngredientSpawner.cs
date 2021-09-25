using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{

    [SerializeField]
    private RectTransform boundsRect;
    [SerializeField]
    private RectTransform noSpawnRect;

    [SerializeField]
    private List<Ingredient> ingredients;
    
    [SerializeField]
    private float spawnDelayInSeconds = .1f;

    [SerializeField] private Transform ingredientSpawnerGroup;
    private List<Ingredient> ingredientPool;
    private float timeRemaining;
    private LevelManager levelManager;



    private void Awake()
    {
        timeRemaining = 2; //initial delay
        //seteo primera vuelta de ingredientes ( aca podemos cambiarlo a antojo para que siempre toque algun ingrediente primero
        //puede ser util para ense�ar la primer pocion a modo de tutorial )
        ingredientPool = new List<Ingredient>(ingredients);
        levelManager = FindObjectOfType<LevelManager>();

    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  
        }
        else
        {
            if (!levelManager.HasPotion)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        //TODO: restricciones de spawn (ejemplo no spawn donde ya hay otro ingrediente )
        //spawnear
        var ingredient = Instantiate(ChooseIngredient(), ChoosePosition(), Quaternion.identity, ingredientSpawnerGroup);
        ingredient.GetComponent<Blink>().StartBlink(3.1f, 2f);

        //resetear timer de cooldown
        float randomness = Random.Range(0,0.65f);
        timeRemaining = spawnDelayInSeconds + randomness ;
    }

    private Ingredient ChooseIngredient()
    {
        int chosenOneIndex = Random.Range(0, ingredientPool.Count);
        var chosenOne = ingredientPool[chosenOneIndex];

        ingredientPool.RemoveAt(chosenOneIndex);

        if(ingredientPool.Count == 0)
        {
            ingredientPool = new List<Ingredient>(ingredients);
        }

        return chosenOne;
    }

    private Vector3 ChoosePosition()
    {
        //pick a position
        Vector3 pos = new Vector3(Random.Range(boundsRect.rect.xMin, boundsRect.rect.xMax), Random.Range(boundsRect.rect.yMin, boundsRect.rect.yMax), 0);

        while (RectTransformUtility.RectangleContainsScreenPoint(noSpawnRect, pos))
        {
             pos = new Vector3(Random.Range(boundsRect.rect.xMin, boundsRect.rect.xMax), Random.Range(boundsRect.rect.yMin, boundsRect.rect.yMax), 0);
        }

        return pos;
    }
}
