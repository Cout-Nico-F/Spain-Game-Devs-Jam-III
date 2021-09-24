using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [SerializeField]
    GameObject pausePanel;

    [SerializeField] private GameObject objectivesPanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        continueButton.onClick.AddListener(Continue);
        ShowObjectives();
    }


    public void ShowObjectives()
    {
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;
        objectivesPanel.SetActive(true);
    }

    public void HideObjectives()
    {
        Time.timeScale = 1;
        StartCoroutine(DoFade(2f));
    }
    
    private void Continue()
    {
        HideObjectives();
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
    
    
    private IEnumerator DoFade(float duration)
    {
        var counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, counter / duration);
            yield return null;
        }
        
        objectivesPanel.SetActive(false);
    }
}
