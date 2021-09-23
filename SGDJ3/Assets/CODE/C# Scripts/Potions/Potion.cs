using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private bool isPressed = false;
    private float releaseTime;
    [SerializeField]
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<SpringJoint2D>().connectedBody = GameObject.FindGameObjectWithTag("Knob").GetComponent<Rigidbody2D>();
        releaseTime = 0.045f;
    }
    private void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
    }
}
