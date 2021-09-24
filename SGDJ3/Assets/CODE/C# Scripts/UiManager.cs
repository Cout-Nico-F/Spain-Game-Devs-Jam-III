using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [SerializeField]
    GameObject pausePanel;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        GameManager.Instance.ToMainMenu();
    }

    
    private void Resume()
    {
        GameManager.Instance.Pause();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
    }
    
    public void UnPause()
    {
        pausePanel.SetActive(false);
    }
}
