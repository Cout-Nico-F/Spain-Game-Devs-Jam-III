using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void StartBlink(float duration, float frequency)
    {
        StartCoroutine(BlinkEffect(duration, frequency));
    }


    private IEnumerator BlinkEffect(float duration, float frequency)
    {
        var elapsed = 0.0f;
        var counter = 0.0f;
        var start = 1.0f;
        var end = 0.0f;
        var color = _renderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            counter += Time.deltaTime;
            var interpolation = counter / (1.0f/frequency);
            
            color.a = Mathf.Lerp(start, end, interpolation);
            _renderer.color = color;
            
            if (interpolation > 1)
            {
                (end, start) = (start, end);
                counter = 0.0f;
            } 
            yield return null;
        }

        color.a = 1;
        _renderer.color = color;
    }
}
