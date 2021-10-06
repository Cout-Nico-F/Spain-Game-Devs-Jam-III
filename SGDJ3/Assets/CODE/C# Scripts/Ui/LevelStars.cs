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
            stars[i].sprite = (i < amount) ? images[0] : images[1];
            
            var color = stars[i].color;
            color.a = 1f;
            stars[i].color = color;
        }
    }
}
