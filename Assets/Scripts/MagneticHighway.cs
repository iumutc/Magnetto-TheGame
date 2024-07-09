using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticHighway : MonoBehaviour
{
    private ObjectPole pole;
    private PlayerMovement player;
    [SerializeField] private int PolePower;
    [SerializeField] private int Polarity;

    private void Start()
    {
        pole = this.gameObject.GetComponent<ObjectPole>();
        switch (pole.currentPole)
        {
            case PlayerMovement.Pole.Positive:
                Polarity = 1;
                break;
            case PlayerMovement.Pole.Negative:
                Polarity = -1;
                break;
            case PlayerMovement.Pole.Neutral:
                Polarity = 0;
                Debug.LogError(this.gameObject.name + " is a Magnetic Highway and Its pole is currently Neutral");
                break;
            default:
                Polarity = 0;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if((player = collision.GetComponent<PlayerMovement>()) != null)
        {
            switch (player.currentPole)
            {
                case PlayerMovement.Pole.Positive:
                    Debug.Log("Positive player");
                    player.GetComponent<Rigidbody2D>().AddForce(this.transform.up * Polarity * PolePower);
                    break;
                case PlayerMovement.Pole.Negative:
                    player.GetComponent<Rigidbody2D>().AddForce(this.transform.up * -Polarity * PolePower);
                    break;
                case PlayerMovement.Pole.Neutral:
                    break;
                default:
                    break;
            }
        }    
    }
}
