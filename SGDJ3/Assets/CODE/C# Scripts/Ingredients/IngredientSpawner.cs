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
    private float spawnDelayInSeconds;
    
    [SerializeField] private Transform ingredientSpawnerGroup;

    private List<Ingredient> ingredientPool;
    private float timeRemaining;
    private LevelManager levelManager;
    private Vector3 ingredientPosition;


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
            if (!levelManager.HasPotion && !levelManager.IsLevelFinish)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        //TODO: restricciones de spawn (ejemplo no spawn donde ya hay otro ingrediente )
        //spawnear

        AudioSystem.Instance.Play("Ingredient Spawn");

        var ingredient = Instantiate(ChooseIngredient(), ChoosePosition(), Quaternion.identity, ingredientSpawnerGroup);
        //movi el blink hacia el ingrediente.
        //resetear timer de cooldown
        float randomness = Random.Range(0,0.65f);
        timeRemaining = spawnDelayInSeconds + randomness;
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
        // cogemos las 4 esquinas del SpawnZone
        var corners = new Vector3[4];
        boundsRect.GetWorldCorners(corners);

        // cogemos el xMin y el yMax de la esquina inferior izquierda
        var xMin = corners[0].x;
        var yMax = corners[0].y;
        
        // cogemos el xMax y el yMin de la esquina superior derecha
        var xMax = corners[2].x;
        var yMin = corners[2].y;
        
        // cogemos las 4 esquinas de la zona de No Spawn
        var cornersNoSpawn = new Vector3[4];
        noSpawnRect.GetWorldCorners(cornersNoSpawn);

        // elegimos una posicion aleatoria dentro de la zona de Spawn
        ingredientPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        
        // si la posicion esta dentro de la NoSpawnZone volvemos a buscar otra posicion
        while (PosWithinNoSpawnZone(ingredientPosition, cornersNoSpawn))
        {
            ingredientPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        }

        return ingredientPosition;
    }

    private bool PosWithinNoSpawnZone(Vector3 position, Vector3[] zone)
    {
        if (position.x >= zone[0].x && position.x <= zone[2].x && position.y >= zone[0].y && position.y <= zone[2].y)
        {
            return true;
        }

        return false;
    }
}
