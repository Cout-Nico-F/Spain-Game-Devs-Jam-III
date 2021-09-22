using UnityEngine;

[CreateAssetMenu(menuName = "New ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    public string id;
    public GameObject prefab;
}
