using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 100f;
    private Vector3 originalScale;
    private Vector3 targetScale = new Vector3(1, 1, 1);
    private float scaleDuration = 0.1f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AnimateScale());
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            rb.velocity = direction * bounceForce;

        }
    }

    private IEnumerator AnimateScale()
    {
        float time = 0;

        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;

        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
    
    
