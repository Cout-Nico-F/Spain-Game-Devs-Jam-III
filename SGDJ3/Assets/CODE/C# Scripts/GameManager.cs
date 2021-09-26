using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;
    private CursorMove cursor;
    private List<string> _partyInvitations;

    public List<string> PartyInvitations { get => _partyInvitations; set => _partyInvitations = value; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _partyInvitations = new List<string>();
        cursor = null;
        ToMainMenu();
    }

    
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            Pause();
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
    }

    
    public void Pause()
    {
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
        SceneManager.LoadScene("Level1");
    }


    public void PlayHistory()
    {
        SceneManager.LoadScene("History");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void InviteToParty(string npcId)
    {
        _partyInvitations.Add(npcId);
    }
}
