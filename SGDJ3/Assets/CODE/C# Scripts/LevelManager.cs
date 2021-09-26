using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelObjective;

    [SerializeField] private Health healthSystem;
    [SerializeField] private FriendsUi friends_ui;
    private int friendCount;

    private int health;

    private int maxHealth = 6;

    private bool hasPotion;
    private bool isLevelFinish;

    public bool IsAnimationFinish;

    [SerializeField]
    private GameObject levelComplete_ui;
    [SerializeField]
    private GameObject levelOver_ui;


    private LevelStars _levelStars;

    public int LevelObjective { get => levelObjective; }
    public int Health { get => health; }
    public bool HasPotion { get => hasPotion; set => hasPotion = value; }

    public bool IsLevelFinish { get => isLevelFinish; set => isLevelFinish = value; }

    private void Awake()
    {
        health = maxHealth;
        hasPotion = false;
        isLevelFinish = false;
        _levelStars = levelComplete_ui.transform.Find("Stars").GetComponent<LevelStars>();

    }

    private void Start()
    {
        AudioSystem.Instance.Play("Gameplay");
    }
    //Este metodo lo llamamos al detectar colision pocion-npc y comprobar que son del mismo color.
    public void FriendJoined()
    {
        friendCount++;
        friends_ui.AddFriend();
        
        if (friendCount >= LevelObjective)
        {
            isLevelFinish = true;
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
            isLevelFinish = true;
            StartCoroutine(LevelOver());
        }

    }

    private IEnumerator LevelCompleted()
    {
        while (!IsAnimationFinish)
        {
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2);

        Debug.Log("WIN");
        levelComplete_ui.SetActive(true);
        _levelStars.SetStars(Mathf.FloorToInt(health/2f));
    }

    private IEnumerator LevelOver()
    {
        while (!IsAnimationFinish)
        {
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2);
        levelOver_ui.SetActive(true);

    }

    public void Retry()
    {
        AudioSystem.Instance.Play("Boton Aceptar");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        AudioSystem.Instance.Play("Boton Aceptar");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
