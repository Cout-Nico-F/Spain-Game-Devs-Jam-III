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
    [SerializeField] private Button backToMenuButton;

    private bool isObjectivesPanelActive;
    public bool IsObjectivesPanelActive => isObjectivesPanelActive;

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        backToMenuButton.onClick.AddListener(BackToMenu);
        continueButton.onClick.AddListener(Continue);
        ShowObjectives();
    }


    public void ShowObjectives()
    {
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;
        objectivesPanel.SetActive(true);
        isObjectivesPanelActive = true;
    }

    public void HideObjectives()
    {
        Time.timeScale = 1;
        StartCoroutine(DoFade(1.5f));
    }
    
    private void Continue()
    {
        AudioSystem.Instance.Play("Boton Aceptar");
        HideObjectives();
    }

    private void BackToMenu()
    {
        GameManager.Instance.ToMainMenu();
    }

    
    private void Resume()
    {
        AudioSystem.Instance.Play("Boton Aceptar");
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
        isObjectivesPanelActive = false;
    }
}
