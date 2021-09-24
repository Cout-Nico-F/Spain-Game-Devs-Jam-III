using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(PlayGame);
        AudioSystem.Instance.Play("History");
    }

    private void PlayGame()
    {
        AudioSystem.Instance.Stop("History");
        GameManager.Instance.StartGame();
    }
}
