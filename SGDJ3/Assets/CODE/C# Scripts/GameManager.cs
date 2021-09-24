using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ToMainMenu();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) )
        {
            Pause();
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

    
    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        //Wait for X seconds
        yield return new WaitForSecondsRealtime(seconds);
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void PlayHistory()
    {
        SceneManager.LoadScene("History");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
