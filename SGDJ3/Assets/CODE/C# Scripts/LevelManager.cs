using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelObjective;

    [SerializeField] private Health healthSystem;

    private int friendCount;

    private int health;

    private int maxHealth = 6;

    private bool hasPotion;

    public bool IsAnimationFinish;

    [SerializeField]
    private GameObject levelComplete_ui;
    [SerializeField]
    private GameObject levelOver_ui;

    private LevelStars _levelStars;

    public int LevelObjective { get => levelObjective; }
    public int Health { get => health; }
    public bool HasPotion { get => hasPotion; set => hasPotion = value; }

    private void Awake()
    {
        health = maxHealth;
        hasPotion = false;
        _levelStars = levelComplete_ui.transform.Find("Stars").GetComponent<LevelStars>();
    }

    //Este metodo lo llamamos al detectar colision pocion-npc y comprobar que son del mismo color.
    public void FriendJoined()
    {
        friendCount++;
        
        if (friendCount >= LevelObjective)
        {
            StartCoroutine(LevelCompleted());
        }
        else
        {
            IsAnimationFinish = false;
        }
    }

    public void Damaged()
    {
        //TODO:desde aca llamar a la animacion de explosion de la olla
        health--;
        healthSystem.TakeDamage();
        //y cambiar sprite de bruja durante unos segundos.
        //UiManager.RefreshUI();
        if (health < 1)
        {
            LevelOver();
        }

    }

    private IEnumerator LevelCompleted()
    {
        while (!IsAnimationFinish)
        {
            yield return null;
        }
        
        Debug.Log("WIN");
        levelComplete_ui.SetActive(true);
        _levelStars.SetStars(Mathf.FloorToInt(health/2f));
    }

    private void LevelOver()
    {
        Debug.Log("LevelOver");
        levelOver_ui.SetActive(true);

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
