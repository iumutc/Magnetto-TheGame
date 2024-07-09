using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private float rotationAmount;
    private float horizontalInput;

    [SerializeField] private ParticleSystem jumpBoostParticle;
    public void JumpBoostParticle()
    {
        jumpBoostParticle.Play();
    }
    [SerializeField] private GameObject speedBostTrail;
    public void SpeedBoostParticle(bool value)
    {
        speedBostTrail.SetActive(value);
    }
   

    [Header("Magnetic Attributes")]
    [SerializeField] private GameObject plus;
    [SerializeField] private GameObject minus;
    private bool isPositivePoleActive;
    private bool isNegativePoleActive;
    private float magneticForce = 50f;
    private List<Rigidbody2D> objectsInRange = new List<Rigidbody2D>();
     
    public enum Pole { Positive, Negative, Neutral }
    public Pole currentPole = Pole.Neutral;
   


    [Header("Movement Attributes")]

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Ground Check")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 2f;
    [SerializeField] private bool isGrounded;
    
    
    
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    
    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }
    
    public float MagneticForce
    {
        get { return magneticForce; }
        set { magneticForce = value; }
    }


    private void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGrounded();

        MovePlayer();
        Debug.Log("Move speed: " + moveSpeed);
        Debug.Log("Jump force: " + jumpForce);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            isPositivePoleActive = !isPositivePoleActive;
            if (isNegativePoleActive)
            {
                isNegativePoleActive = false;
            }
            
            Debug.Log("Positive Pole Active: " + isPositivePoleActive);
            
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            isNegativePoleActive = !isNegativePoleActive;
            if (isPositivePoleActive)
            {
                isPositivePoleActive = false;
            }
            
            Debug.Log("Negative Pole Active: " + isNegativePoleActive);
        }

        if (isPositivePoleActive)
        {
            currentPole = Pole.Positive;
            Attract();
        }
        else if (isNegativePoleActive)
        {
            currentPole = Pole.Negative;
            Repel();
        }
        else
        {
            currentPole = Pole.Neutral;
        }

        handleVisual();
        
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime * rigidbody2D.velocity.sqrMagnitude;

            this.rigidbody2D.AddForce(new Vector2(moveSpeed * horizontalInput, 0));
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime * rigidbody2D.velocity.sqrMagnitude;

            this.rigidbody2D.AddForce(new Vector2(moveSpeed * horizontalInput * 0.25f, 0));
        }
 
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(new Vector2(this.transform.position.x, (this.transform.position.y - this.transform.localScale.y / 2) - 0.1f), -Vector2.up, groundDistance);
    }

    void Jump()
    {
        this.rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        this.transform.DOScale(new Vector3(2.3f,2.3f,2.3f),0.3f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.InOutSine);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Movable") || other.gameObject.CompareTag("Immovable"))
        {
            objectsInRange.Add(other.GetComponent<Rigidbody2D>());
            
            Debug.Log("Object in range");
        }
        if (other.gameObject.CompareTag("MagneticHighway"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Movable") || other.gameObject.CompareTag("Immovable"))
        {
            objectsInRange.Remove(other.GetComponent<Rigidbody2D>());
            
            Debug.Log("Object out of range");
        }
    }

void Attract()
{
    foreach (Rigidbody2D target in objectsInRange)
    {
        Pole targetPole = target.GetComponent<ObjectPole>().currentPole;

        if ((currentPole == Pole.Positive && targetPole == Pole.Negative) ||
            (currentPole == Pole.Negative && targetPole == Pole.Positive))
        {
            Vector2 direction = ((Vector2)transform.position - target.position).normalized;
            ApplyForceToTarget(target, direction);
            
            Debug.Log("Attracting inside Attract method");
        }
        else if (currentPole == targetPole)
        {
            Vector2 direction = (target.position - (Vector2)transform.position).normalized;
            ApplyForceToTarget(target, direction);
            
            Debug.Log("Repelling inside Attract method");
        }
    }
}

void Repel()
{
    foreach (Rigidbody2D target in objectsInRange)
    {
        Pole targetPole = target.GetComponent<ObjectPole>().currentPole;

        if ((currentPole == Pole.Positive && targetPole == Pole.Negative) ||
            (currentPole == Pole.Negative && targetPole == Pole.Positive))
        {
            Vector2 direction = ((Vector2)transform.position - target.position).normalized;
            ApplyForceToTarget(target, direction);
            
            Debug.Log("Attracting inside Repel method");
        }
        else if (currentPole == targetPole)
        {
            Vector2 direction = (target.position - (Vector2)transform.position).normalized;
            ApplyForceToTarget(target, direction);
            
            Debug.Log("Repelling inside Attract method");
        }
    }
}

void ApplyForceToTarget(Rigidbody2D target, Vector2 direction)
{
    if (target.gameObject.CompareTag("Movable"))
    {
        target.AddForce(direction * magneticForce);
    }
    else if (target.gameObject.CompareTag("Immovable"))
    {
        rigidbody2D.AddForce(-direction * magneticForce);
    }
}


    void handleVisual()
    {
        switch (this.currentPole)
        {
            case Pole.Positive:

                plus.SetActive(true);
                minus.SetActive(false);

                spriteRenderer.color = new Color(0.8301887f, 0.3798505f, 0.3798505f);

                break;
            case Pole.Negative:

                plus.SetActive(false);
                minus.SetActive(true);

                spriteRenderer.color = new Color(0.389596f, 0.729283f, 0.8018868f);

                break;
            default:

                plus.SetActive(false);
                minus.SetActive(false);

                spriteRenderer.color = new Color(0.4905f,0.4095f,0.4096f);
                break;
        }
    }

}