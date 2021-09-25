using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
            // si colisiona con la pocion rosa cambia el estado a otro aleatorio
            if (collision.GetComponent<Potion>().color.Equals("pink"))
            {
                _state.color = colors[Random.Range(0, colors.Length - 1)];
                _stateVisual.sprite = dict[_state.color];
                return;
            }
            
            if (collision.GetComponent<Potion>().color.Equals(_state.color))
            {
                _stateVisual.sprite = images[4];
                GameObject.FindObjectOfType<LevelManager>().FriendJoined();
                
                // TODO: animacion de contento o parpadeo y desaparecer 
                Debug.Log("Cured, he wants to join the party!");

            }
            else
            {
                // TODO: ¿alguna animacion de enfado? ¿se va del bosque?
                Debug.Log("Exploding recipe.");
            }
        }
    }
}
