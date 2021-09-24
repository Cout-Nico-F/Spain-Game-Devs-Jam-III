using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    UiManager uiManager;
    private bool isPaused;

    private void Start()
    {
        uiManager = GameObject.FindObjectOfType<UiManager>();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            uiManager.Pause();
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            uiManager.UnPause();
        }
    }

    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        //Wait for X seconds
        yield return new WaitForSecondsRealtime(seconds);
    }


}
