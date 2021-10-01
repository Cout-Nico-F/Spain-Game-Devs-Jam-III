using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "Level1", order = 1)]
public class LevelAsset : ScriptableObject
{
    public int levelObjective;
    public GameObject[] npcPrefabs;
    public Vector3[] npcPositions;
}
