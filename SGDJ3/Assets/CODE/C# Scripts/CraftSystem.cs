﻿using UnityEngine;

public class CraftSystem : Singleton<CraftSystem>
{
    [SerializeField] private Recipe[] recipes;
    [SerializeField] private Transform potionSpawnPoint;
    [SerializeField] private GameObject Poof_prefab;

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

        // aqui se podria instanciar el efecto de puff y que la pocion esperase a que terminara para instanciarse
        Instantiate(Poof_prefab, ingredient2.transform.position, Quaternion.identity);
        Destroy(Poof_prefab, 2);
        Instantiate(potion, potionSpawnPoint.position, Quaternion.identity);
    }

    public void WrongMix(Ingredient ingredient1, Ingredient ingredient2)
    {
        GameObject.FindObjectOfType<LevelManager>().Damaged();

        Destroy(ingredient1.gameObject);
        Destroy(ingredient2.gameObject);
    }
}

