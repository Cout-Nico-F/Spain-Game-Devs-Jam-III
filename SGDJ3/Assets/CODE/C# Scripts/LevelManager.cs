using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelAsset[] _levels;
    [SerializeField] private Health healthSystem;
    [SerializeField] private FriendsUi friends_ui;
    [SerializeField] private GameObject levelComplete_ui;
    [SerializeField] private GameObject levelOver_ui;

    private int currentLevel = 1;
    private int friendCount;
    private int health;
    private int maxHealth = 6;
    private bool hasPotion;
    private bool isLevelFinish;
    private LevelStars _levelStars;

    public int CurrentLevel => currentLevel; 
    public int LevelObjective { get => _levels[currentLevel-1].levelObjective; }
    public int Health { get => health; }
    public bool HasPotion { get => hasPotion; set => hasPotion = value; }

    public bool IsLevelFinish { get => isLevelFinish; set => isLevelFinish = value; }

    private void Start()
    {
        currentLevel = 1;
        health = maxHealth;
        hasPotion = false;
        isLevelFinish = false;
        _levelStars = levelComplete_ui.transform.Find("Stars").GetComponent<LevelStars>();
        AudioSystem.Instance.Play("Gameplay");
        SpawnNpc();
    }

    private void SpawnNpc()
    {
        for (int i = 0; i < _levels[currentLevel-1].npcPrefabs.Length; i++)
        {
            Instantiate(_levels[currentLevel - 1].npcPrefabs[i], _levels[currentLevel - 1].npcPositions[i],
                Quaternion.identity);
        }
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
            AudioSystem.Instance.Play("Level Lost");
            StartCoroutine(LevelOver());
        }

    }

    private IEnumerator LevelCompleted()
    {
        yield return new WaitForSecondsRealtime(2);

        Debug.Log("WIN");
        AudioSystem.Instance.Play("Level Won");
        levelComplete_ui.SetActive(true);
        _levelStars.SetStars(Mathf.FloorToInt(health/2f));
    }

    private IEnumerator LevelOver()
    {
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
