using DG.Tweening;
using UnityEngine;
using Sequence = Unity.VisualScripting.Sequence;

public class EndLevelTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().OnReachFinalSpot();
        }
    }
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(spriteRenderer.DOColor(Color.white, 0.5f));
        sequence.Append(spriteRenderer.DOColor(Color.black, 0.5f));
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }
}