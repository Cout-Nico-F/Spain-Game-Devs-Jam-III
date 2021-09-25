using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _stateVisual;
    [SerializeField] private State _state;
    [SerializeField] private Sprite[] images;
    [SerializeField] private string[] colors;
    [SerializeField] private GameObject poofPotionOKPrefab;
    private Animator poofAnimator;
    private Dictionary<string, Sprite> dict;
    private SpriteRenderer myRenderer;
    private LevelManager _levelManager;
    private float timer;
    private float timerReset = 3;


    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        myRenderer = GetComponent<SpriteRenderer>();
        timer = timerReset + Random.Range(0,3);

        dict = new Dictionary<string, Sprite>();
        for (var i = 0; i < 4; i++)
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
            timer = timerReset + Random.Range(0, 3);
            Flip();
        }
    }

    private void Flip()
    {
        myRenderer.flipX = !myRenderer.flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion"))
        {
            // si colisiona con la pocion rosa cambia el estado a otro aleatorio
            if (collision.GetComponent<Potion>().color.Equals("pink"))
            {
                PotionOKEffect();
                _state.color = colors[Random.Range(0, 4)];
                _stateVisual.sprite = dict[_state.color];
                return;
            }
            
            if (collision.GetComponent<Potion>().color.Equals(_state.color))
            {
                PotionOKEffect();
                _stateVisual.sprite = images[Random.Range(4, 6)];
                _levelManager.FriendJoined();
                
                Debug.Log("Cured, he wants to join the party!");
            }
            else
            {
                // TODO: ¿alguna animacion de enfado? ¿se va del bosque?
                Debug.Log("Exploding recipe.");
            }
        }
    }

    private void PotionOKEffect()
    {
        var poof = Instantiate(poofPotionOKPrefab, transform.position, Quaternion.identity);
        poofAnimator = poof.GetComponent<Animator>();
        StartCoroutine(FinishAnimation());
        Destroy(poof, 2);
    }
    
    private IEnumerator FinishAnimation()
    {
        while (poofAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        
        _levelManager.IsAnimationFinish = true;
    }
    
}
