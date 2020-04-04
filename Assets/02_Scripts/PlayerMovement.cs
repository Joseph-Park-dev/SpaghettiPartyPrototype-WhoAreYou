using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private Transform feetPos;
    private float checkRadius = .3f;
    private Collider2D[] steppingColl;
    [SerializeField] private LayerMask objectStepping;

    private Vector2 characterScale;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        characterScale = transform.localScale;
    }
    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        Vector2 direction = new Vector2(inputH, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        AnimateHorizontal(inputH);
    }

    private void AnimateHorizontal(float inputH)
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(inputH));
        Flip(inputH);
    }

    private void Flip(float inputH)
    {
        if (inputH < 0)
            characterScale.x = -1;
        else
            characterScale.x = 1;
        transform.localScale = characterScale;
    }

    private void Jump()
    {
        steppingColl = Physics2D.OverlapCircleAll(
            feetPos.position,
            checkRadius,
            objectStepping
            );
        foreach(Collider2D collider in steppingColl)
        {
            bool onGround = collider.gameObject.CompareTag("GROUND");
            if (onGround && Input.GetKeyDown(KeyCode.Space))
                rb.velocity = Vector2.up * jumpForce;
            bool onPlayer = collider.gameObject.CompareTag("ENEMY");
            if (onPlayer && !onGround)
            {
                print("hello!");
                rb.velocity = Vector2.up * bounceForce;
            }
            AnimateVertical(onGround);
        }
    }
/*
    private bool StompDetected()
    {
        if(isStepping == true && isStepping.gameObject.tag == "ENEMY")
        {
            print("Detected!");
            return true;
        }
        return false;
    }
    */

    private void AnimateVertical(bool isStepping)
    { 
        animator.SetBool("isStepping", isStepping);
    }
    
    
}
