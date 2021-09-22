using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public string id;
    public List<Ingredient> ingredients;
}
