using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;
    [SerializeField]
    private Texture2D pressedCursor;
    [SerializeField]
    private Texture2D defaultCursor;

    private List<string> _partyInvitations;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _partyInvitations = new List<string>();
        ToMainMenu();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) )
        {
            Pause();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pressedCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursor, new Vector2(16,16), CursorMode.Auto);
        }
    }

    
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    
    public void Pause()
    {
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

        foreach (var npc in _partyInvitations)
        {
            Debug.Log(npc);
        }
    }
}
