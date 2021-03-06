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
    
    private Transform npcSpawnPositions;
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
        npcSpawnPositions = GameObject.FindWithTag("NpcSpawnPoints").transform;
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
                AudioSystem.Instance.Play("Impacto Especial");
                PotionSwitchEffect();
                var previousColor = _state.color;
                var switchColor = colors[Random.Range(0, 4)];
                while (switchColor.Equals(previousColor))
                {
                    switchColor = colors[Random.Range(0, 4)];
                }

                _state.color = switchColor;
                _stateVisual.sprite = dict[_state.color];
                return;
            }
            
            if (collision.GetComponent<Potion>().color.Equals(_state.color))
            {
                AudioSystem.Instance.Play("Impacto Bueno");
                GameManager.Instance.InviteToParty(npcId);
                PotionOKEffect();
                _stateVisual.sprite = images[Random.Range(4, 6)];
                GetComponent<Blink>().StartBlink(3f, 4f);
                Destroy(gameObject, 3.5f);
                _levelManager.FriendJoined();
                
                Debug.Log("Cured, he wants to join the party!");
            }
            else
            {
                PotionFailEffect();
                AudioSystem.Instance.Play("Impacto Malo");
                GetComponent<Blink>().StartBlink(3.1f, 4f);
                StartCoroutine(ChangeNpcPosition());
            }
        }
    }

    private IEnumerator ChangeNpcPosition()
    {
        yield return new WaitForSeconds(3f);
        var index = Random.Range(0, npcSpawnPositions.childCount);
        var currentPosition = transform.position;
        var switchPosition = npcSpawnPositions.GetChild(index).position;
        
        npcSpawnPositions.GetChild(index).position = currentPosition;
        transform.position = switchPosition;
    }


    private void PotionOKEffect()
    {
        var poof = Instantiate(poofPotionOKPrefab, effectSpawnPoint.position, Quaternion.identity);
        poofAnimator = poof.GetComponent<Animator>();
        Destroy(poof, 2.1f);
    }
    
    
    private void PotionSwitchEffect()
    {
        var poof = Instantiate(poofPotionSwitchPrefab, effectSpawnPoint.position, Quaternion.identity);
        poofAnimator = poof.GetComponent<Animator>();
        Destroy(poof, 2f);
    }
    
    
    private void PotionFailEffect()
    {
        var poof = Instantiate(poofPotionFailPrefab, effectSpawnPoint.position, Quaternion.identity);
        Destroy(poof, 2f);
    }
    
}
