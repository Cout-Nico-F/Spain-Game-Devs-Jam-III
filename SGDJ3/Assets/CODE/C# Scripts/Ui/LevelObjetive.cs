using UnityEngine;
using UnityEngine.UI;

public class LevelObjetive : MonoBehaviour
{
    [SerializeField] private Text levelObjectiveText;

    private void Start()
    {
        var levelObjective = FindObjectOfType<LevelManager>().LevelObjective;
        levelObjectiveText.text =
            "Objetivo de dia: Curar " + levelObjective + (levelObjective > 1 ? " emociones" : " emocion");
    }
}
