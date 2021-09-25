using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    
    private Image[] hearts;
    private int heartNumber;
    private bool fullHeart;


    private void Awake()
    {
        hearts = GetComponentsInChildren<Image>();
        heartNumber = 2;
        fullHeart = true;
    }

    
    public void TakeDamage()
    {
        if (fullHeart)
        {
            hearts[heartNumber].sprite = images[1];
        }
        else
        {
            hearts[heartNumber].sprite = null;
            var color = hearts[heartNumber].color;
            color.a = 0;
            hearts[heartNumber].color = color;
            heartNumber--;
            if (heartNumber < 0)
            {
                Debug.Log("Level Over");
                return;
            }
        }
        fullHeart = !fullHeart;
    }
}
