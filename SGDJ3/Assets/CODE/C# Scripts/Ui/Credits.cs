using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Button backToMenu;

    private void Awake()
    {
        backToMenu.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        AudioSystem.Instance.Play("Boton");
        GameManager.Instance.ToMainMenu();
    }
}
