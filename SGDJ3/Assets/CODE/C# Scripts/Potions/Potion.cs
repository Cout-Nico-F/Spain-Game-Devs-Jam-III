using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string color;

    [SerializeField]
    private Rigidbody2D rb;

    private bool isPressed = false;
    private float releaseTime;
    private float maxDragDistance = 4f;
    private LevelManager levelManager;
    private Rigidbody2D knob_Rb;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<SpringJoint2D>().connectedBody = GameObject.FindGameObjectWithTag("Knob").GetComponent<Rigidbody2D>();
        releaseTime = 0.045f;

        levelManager = FindObjectOfType<LevelManager>();
        knob_Rb  = GameObject.FindGameObjectWithTag("Knob").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, knob_Rb.position) > maxDragDistance)
            {
                rb.position = knob_Rb.position + (mousePos - knob_Rb.position).normalized * maxDragDistance;
            }
            else rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private Rigidbody2D FindConnectedBody()
    {
        return new Rigidbody2D();
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
        levelManager.HasPotion = false;

        AudioSystem.Instance.Play("Lanzar Pocion");
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
    }
}
