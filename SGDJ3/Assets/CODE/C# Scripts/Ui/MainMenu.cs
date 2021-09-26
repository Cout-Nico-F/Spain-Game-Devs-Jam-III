using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _QuitGameButton;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _creditsButton.onClick.AddListener(GoToCredits);
        _QuitGameButton.onClick.AddListener(QuitGame);

    }

    private void Start()
    {
        AudioSystem.Instance.Play("Main Menu");
    }

    private void GoToCredits()
    {
        GameManager.Instance.GoToCredits();
    }

    private void StartGame()
    {
        GameManager.Instance.PlayHistory();
    }

    
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();       
#endif        
    }

}
