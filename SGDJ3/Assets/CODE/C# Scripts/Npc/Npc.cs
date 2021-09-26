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
    [SerializeField] private GameObject poofPotionFailPrefab;
    [SerializeField] private GameObject poofPotionSwitchPrefab;
    [SerializeField] private string npcId;
    
    private Transform effectSpawnPoint;
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
        effectSpawnPoint = transform.Find("effectSpawnPoint");
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
            Destroy(collision.gameObject);
            
            // si colisiona con la pocion rosa cambia el estado del Npc a otro aleatorio
            if (collision.GetComponent<Potion>().color.Equals("pink"))
            {
                PotionSwitchEffect();
                _state.color = colors[Random.Range(0, 4)];
                _stateVisual.sprite = dict[_state.color];
                GetComponent<Blink>().StartBlink(3f, 4f);
                return;
            }
            
            if (collision.GetComponent<Potion>().color.Equals(_state.color))
            {
                GameManager.Instance.InviteToParty(npcId);
                PotionOKEffect();
                _stateVisual.sprite = images[Random.Range(4, 6)];
                GetComponent<Blink>().StartBlink(3f, 4f);
                _levelManager.FriendJoined();
                
                Debug.Log("Cured, he wants to join the party!");
            }
            else
            {
                PotionFailEffect();
                GetComponent<Blink>().StartBlink(3f, 4f);
                //move npc to random position inside certain area.
            }
        }
    }

    
    private void PotionOKEffect()
    {
        var poof = Instantiate(poofPotionOKPrefab, effectSpawnPoint.position, Quaternion.identity);
        poofAnimator = poof.GetComponent<Animator>();
        StartCoroutine(FinishAnimation());
        Destroy(poof, 2.1f);
    }
    
    
    private void PotionSwitchEffect()
    {
        var poof = Instantiate(poofPotionSwitchPrefab, effectSpawnPoint.position, Quaternion.identity);
        poofAnimator = poof.GetComponent<Animator>();
        StartCoroutine(FinishAnimation());
        Destroy(poof, 2f);
    }
    
    
    private void PotionFailEffect()
    {
        var poof = Instantiate(poofPotionFailPrefab, effectSpawnPoint.position, Quaternion.identity);
        Destroy(poof, 2f);
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
