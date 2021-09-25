using System;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _stateVisual;
    [SerializeField] private State _state;
    [SerializeField] private Sprite[] images;
    [SerializeField] private string[] colors;
    private Dictionary<string, Sprite> dict;


    private void Awake()
    {
        dict = new Dictionary<string, Sprite>();
        for (var i = 0; i < colors.Length; i++)
        {
            dict.Add(colors[i], images[i]);
        }
    }

    private void Start()
    {
        if (_state != null)
        {
            _stateVisual.sprite = dict[_state.color];
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion"))
        {
            if (collision.GetComponent<Potion>().color.Equals(_state.color))
            {
                GameObject.FindObjectOfType<LevelManager>().FriendJoined();
                Debug.Log("Cured, he wants to join the party!");

            }
            else
            {
                Debug.Log("Exploding recipe.");
            }
        }
    }
}
