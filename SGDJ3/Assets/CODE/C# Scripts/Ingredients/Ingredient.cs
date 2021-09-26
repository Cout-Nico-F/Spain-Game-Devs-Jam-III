using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string id;
    private bool isPressed;
    private float timeRemaining;

    private  float timeLife = 5.1f; //Tiempo que duran los ingredientes antes de desaparecer

    private void Awake()
    {
        float randomness = Random.Range(0, 0.65f);
        timeRemaining = timeLife;
    }

    private void LateUpdate()
    {
        Color color = this.GetComponent<SpriteRenderer>().color;

        if (isPressed)
        {
            color.a = 1;
            this.GetComponent<SpriteRenderer>().color = color;
            return;
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            return;
        }
        else
        {
            if (timeRemaining < 2.11f)
            {
                this.GetComponent<Blink>().StartBlink(2.1f, 6.3f);
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        timeRemaining = timeLife;
    }

}
