using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoleVisual : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject plus;
    [SerializeField] private GameObject minus;

    private float[] r = { 0.490566f };
    private float[] g = { 0.490566f };
    private float[] b = { 0.490566f };

    private void Start()
    {
        
    }
    void Update()
    {
        plus.GetComponent<SpriteRenderer>().color = new Color(r[0], g[0], b[0]);
    }
}
