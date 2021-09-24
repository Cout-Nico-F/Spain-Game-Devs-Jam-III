using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    private State _state;

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
