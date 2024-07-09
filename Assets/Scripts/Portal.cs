using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform positiveDestination;
    public Transform negativeDestination;
    public Transform neutralDestination;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            switch (player.currentPole)
            {
                case PlayerMovement.Pole.Positive:
                    player.transform.position = positiveDestination.position;
                    break;
                case PlayerMovement.Pole.Negative:
                    player.transform.position = negativeDestination.position;
                    break;
                default:
                    player.transform.position = neutralDestination.position;
                    break;
            }
        }
    }
}
