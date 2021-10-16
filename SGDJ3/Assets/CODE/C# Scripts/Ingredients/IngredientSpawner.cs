using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    private Vector3[] corners;
    private Vector3[] cornersNoSpawn;
    private float xMin;
    private float yMax;
    private float xMax;
    private float yMin;


    private void Start()
    {
        timeRemaining = 2; //initial delay
        //seteo primera vuelta de ingredientes ( aca podemos cambiarlo a antojo para que siempre toque algun ingrediente primero
        //puede ser util para ense�ar la primer pocion a modo de tutorial )
        ingredientPool = new List<Ingredient>(ingredients);
        levelManager = FindObjectOfType<LevelManager>();
        GetBounds();
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  
        }
        else
        {
            if (!levelManager.IsLevelFinish)
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
        // elegimos una posicion aleatoria dentro de la zona de Spawn
        ingredientPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);

        // si la posicion esta dentro de la NoSpawnZone o colisiona con algo
        // volvemos a buscar otra posicion
        while (IsColliding(ingredientPosition, 0.6f) || PosWithinNoSpawnZone(ingredientPosition, cornersNoSpawn))
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

    private void GetBounds()
    {
        // cogemos las 4 esquinas del SpawnZone
        corners = new Vector3[4];
        boundsRect.GetWorldCorners(corners);

        // cogemos el xMin y el yMax de la esquina inferior izquierda
        xMin = corners[0].x;
        yMax = corners[0].y;
        
        // cogemos el xMax y el yMin de la esquina superior derecha
        xMax = corners[2].x;
        yMin = corners[2].y;
        
        // cogemos las 4 esquinas de la zona de No Spawn
        cornersNoSpawn = new Vector3[4];
        noSpawnRect.GetWorldCorners(cornersNoSpawn);
    }

    private bool IsColliding(Vector2 point, float radius)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point, radius);
        return collider2Ds.Length > 0;
    }
}
