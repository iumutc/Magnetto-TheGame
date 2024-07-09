using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.DOColor(Color.red, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * 500f, ForceMode2D.Impulse);
    }
}
