using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _stateVisual;
    [SerializeField] private State _state;
    [SerializeField] private Sprite[] images;
    [SerializeField] private string[] colors;
    private Dictionary<string, Sprite> dict;
    private float timer;
    private float timerReset = 2;


    private void Awake()
    {
        timer = timerReset + Random.Range(0,2);

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

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timerReset + Random.Range(0, 2);
            Flip();
        }
    }

    private void Flip()
    {
        if (this.transform.eulerAngles.y == -180)
        {
            this.transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            this.transform.Rotate(new Vector3(0, 180, 0));
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
