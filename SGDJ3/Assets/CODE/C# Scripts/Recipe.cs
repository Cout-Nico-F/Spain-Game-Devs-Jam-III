using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Nueva receta")]
public class Recipe : ScriptableObject
{
    public string id;
    public List<Ingrediente> ingredientes;
}
