using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        GameManager.Instance.StartGame();
    }
}
