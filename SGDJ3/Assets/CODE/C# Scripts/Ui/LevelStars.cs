using UnityEngine;
using UnityEngine.UI;

public class LevelStars : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    
    private Image[] stars;


    private void Awake()
    {
        stars = GetComponentsInChildren<Image>();
    }


    public void SetStars(int amount)
    {
        for (var i = 0; i < stars.Length; i++)
        {
            if (amount <= i) continue;
            
            stars[i].sprite = images[0];
            var color = stars[i].color;
            color.a = 1f;
            stars[i].color = color;
        }
    }
}
