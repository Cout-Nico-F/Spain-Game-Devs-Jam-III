using UnityEngine;
using UnityEngine.UI;

public class LevelObjetive : MonoBehaviour
{
    [SerializeField] private Text levelObjectiveText;
    [SerializeField] private Text levelName;
    private LevelManager _levelManager;
    
    private void OnEnable()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        levelName.text = "Nivel " + _levelManager.CurrentLevel;
        var levelObjective = FindObjectOfType<LevelManager>().LevelObjective;
        levelObjectiveText.text =
            "Objetivo de dia: Curar " + levelObjective + (levelObjective > 1 ? " emociones" : " emocion");
    }
}
