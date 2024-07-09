using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPole : MonoBehaviour
{
    public PlayerMovement.Pole currentPole;

    public GameObject child;
    
    public bool isChangingPole;

    private void Start()
    {
        if (isChangingPole)
        {
            StartCoroutine(ChangeColorAndPolarity());
        }
    }
    

    private IEnumerator ChangeColorAndPolarity()
    {
        while (true)
        {
            switch (currentPole)
            {
                case PlayerMovement.Pole.Positive:
                    Debug.Log("Positive");
                    child.GetComponent<SpriteRenderer>().color = new Color(0.389596f, 0.729283f, 0.8018868f);
                    currentPole = PlayerMovement.Pole.Negative;
                    break;
                case PlayerMovement.Pole.Negative:
                    child.GetComponent<SpriteRenderer>().color = new Color(0.8301887f, 0.3798505f, 0.3798505f);
                    currentPole = PlayerMovement.Pole.Positive;
                    break;
            }

            yield return new WaitForSeconds(3f);
        }
    }
    



}
