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
            var color = hearts[heartNumber].color;
            color.a = 0;
            hearts[heartNumber].color = color;
            heartNumber--;
        }
        fullHeart = !fullHeart;
    }


    public void ResetLive()
    {
        heartNumber = 2;
        fullHeart = true;
        foreach (var heart in hearts)
        {
            heart.sprite = images[0];
            var color = heart.color;
            color.a = 1;
            heart.color = color;
        }
    }
}
