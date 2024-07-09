using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float playerDetectionDistance = 1.5f;

    private int currentPointIndex = 0;

    void Start()
    {
        StartCoroutine(MoveToNextPoint());
    }

    IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            Vector2 direction = ((Vector2)points[currentPointIndex].position - (Vector2)transform.position).normalized;

            while (Vector2.Distance(transform.position, points[currentPointIndex].position) > 0.1f)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);


                yield return null;
            }

            currentPointIndex = (currentPointIndex + 1) % points.Length;

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.transform.parent = null;
        }
    }
}
