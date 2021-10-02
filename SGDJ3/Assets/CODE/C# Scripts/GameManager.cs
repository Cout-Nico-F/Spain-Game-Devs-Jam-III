using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;
    private CursorMove cursor;
    private List<string> _partyInvitations;
    private bool isGamePlay;


    public List<string> PartyInvitations { get => _partyInvitations; set => _partyInvitations = value; }

    public bool IsGamePlay { get => isGamePlay; set => isGamePlay = value; }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _partyInvitations = new List<string>();
        cursor = null;
        ToMainMenu();
        isGamePlay = false;
    }

    
    private void Update()
    {
        if (isGamePlay)
        {
            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && SceneManager.GetActiveScene().name.Equals("Gameplay"))
            {
                Pause();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (cursor == null) cursor = GameObject.FindWithTag("Cursor").GetComponent<CursorMove>();
            
            cursor.SetCursorPressed();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (cursor == null) cursor = GameObject.FindWithTag("Cursor").GetComponent<CursorMove>();
            
            cursor.SetCursorDefault();
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        isGamePlay = false;
    }

    
    public void Pause()
    {
        AudioSystem.Instance.Play("Boton");

        if (UiManager.Instance.IsObjectivesPanelActive) return;
        
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            UiManager.Instance.Pause();
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            UiManager.Instance.UnPause();
        }
    }

    
    public IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        //Wait for X seconds
        yield return new WaitForSecondsRealtime(seconds);
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        isGamePlay = true;
    }


    public void PlayHistory()
    {
        SceneManager.LoadScene("History");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
        isGamePlay = false;
    }

    public void InviteToParty(string npcId)
    {
        _partyInvitations.Add(npcId);
    }
}
