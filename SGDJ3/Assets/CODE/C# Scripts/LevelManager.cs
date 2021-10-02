using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelAsset[] _levels;
    [SerializeField] private Health healthSystem;
    [SerializeField] private FriendsUi friends_ui;
    [SerializeField] private GameObject levelComplete_ui;
    [SerializeField] private GameObject levelOver_ui;
    [SerializeField] private Text levelCompleteText;
    [SerializeField] private Text levelOverText;

    private int currentLevel = 1;
    private int friendCount;
    private int health;
    private int maxHealth = 6;
    private bool hasPotion;
    private bool isLevelFinish;
    private LevelStars _levelStars;
    private List<GameObject> _liveNpcs;

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
        _liveNpcs = new List<GameObject>();
        SpawnNpc();
    }

    private void SpawnNpc()
    {
        for (int i = 0; i < _levels[currentLevel-1].npcPrefabs.Length; i++)
        {
            GameObject npc = Instantiate(_levels[currentLevel - 1].npcPrefabs[i], _levels[currentLevel - 1].npcPositions[i],
                Quaternion.identity);
            _liveNpcs.Add(npc);
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
            GameManager.Instance.IsGamePlay = false;
            AudioSystem.Instance.Play("Level Lost");
            StartCoroutine(LevelOver());
        }

    }

    private IEnumerator LevelCompleted()
    {
        yield return new WaitForSecondsRealtime(2);

        Debug.Log("WIN");
        GameManager.Instance.IsGamePlay = false;
        AudioSystem.Instance.Play("Level Won");
        levelCompleteText.text = "Nivel " + currentLevel;
        levelComplete_ui.SetActive(true);
        _levelStars.SetStars(Mathf.FloorToInt(health/2f));
        DestroyLiveNpcs();
    }

    private void DestroyLiveNpcs()
    {
        foreach (var liveNpc in _liveNpcs)
        {
            Destroy(liveNpc);
        }

        _liveNpcs.Clear();
    }

    private IEnumerator LevelOver()
    {
        yield return new WaitForSecondsRealtime(2);
        levelOverText.text = "Nivel " + currentLevel;
        levelOver_ui.SetActive(true);
        DestroyLiveNpcs();
    }

    public void Retry()
    {
        AudioSystem.Instance.Play("Boton Aceptar");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // en lugar de cargar de nuevo la escena, debemos inicializar la escena
        // de Gameplay cargando el mismo nivel
        levelOver_ui.SetActive(false);
        AudioSystem.Instance.Stop("Level Lost");
        ResetLevel();
    }

    public void NextLevel()
    {
        AudioSystem.Instance.Play("Boton Aceptar");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        // en lugar de cargar una nueva escena, debemos inicializar la escena de Gameplay cargando
        // el nuevo nivel
        levelComplete_ui.SetActive(false);
        AudioSystem.Instance.Stop("Level Won");
        currentLevel++;
        if (currentLevel > _levels.Length)
        {
            SceneManager.LoadScene("Party");
            return;
        }

        ResetLevel();
    }

    private void ResetLevel()
    {
        friendCount = 0;
        health = maxHealth;
        healthSystem.ResetLive();
        friends_ui.ResetFriends();
        hasPotion = false;
        isLevelFinish = false;
        AudioSystem.Instance.Play("Gameplay");
        SpawnNpc();
        UiManager.Instance.ShowObjectives();
    }
}
