using System.Collections;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string color;

    private bool isPressed = false;
    private float releaseTime;
    private float maxDragDistance = 4f;
    private LevelManager levelManager;
    private Rigidbody2D rb;
    private bool isPotionFlying;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        releaseTime = 0.045f;
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, rb.position) > maxDragDistance)
            {
                rb.position = rb.position + (mousePos - rb.position).normalized * maxDragDistance;
            }
            else rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    

    private void OnMouseDown()
    {
        if (isPotionFlying) return;

        AudioSystem.Instance.Play("Agarrar Tirachinas");
        isPressed = true;
        isPotionFlying = false;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPotionFlying = true;
        //AudioSystem.Instance.Play("Lanzar Pocion");
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
        levelManager.HasPotion = false;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
    }
}
