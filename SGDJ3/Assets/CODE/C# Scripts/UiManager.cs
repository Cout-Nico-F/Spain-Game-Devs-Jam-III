using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    [SerializeField]
    GameObject pausePanel;
    
    public void Pause()
    {
        pausePanel.SetActive(true);
    }
    
    public void UnPause()
    {
        pausePanel.SetActive(false);
    }
}
