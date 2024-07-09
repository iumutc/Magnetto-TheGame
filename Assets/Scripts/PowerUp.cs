using System.Collections;
using UnityEngine;

using DG.Tweening;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Jump, MagneticForce }
    public PowerUpType type;
    public float duration = 5f; 
    public float respawnTime = 10f; 
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            StartCoroutine(ApplyPowerUp(player));
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator ApplyPowerUp(PlayerMovement player)
    {
        switch (type)
        {
            case PowerUpType.Speed:
                player.SpeedBoostParticle(true);
                player.MoveSpeed *= 2;
                break;
            case PowerUpType.Jump:
                player.JumpBoostParticle();
                player.JumpForce *= 2;
                break;
            case PowerUpType.MagneticForce:
                player.MagneticForce *= 2;
                break;
        }

        yield return new WaitForSeconds(duration);

        switch (type)
        {
            case PowerUpType.Speed:
                player.SpeedBoostParticle(false);
                player.MoveSpeed /= 2;
                break;
            case PowerUpType.Jump:
                player.JumpForce /= 2;
                break;
            case PowerUpType.MagneticForce:
                player.MagneticForce /= 2;
                break;
        }
        
    }

    private IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}