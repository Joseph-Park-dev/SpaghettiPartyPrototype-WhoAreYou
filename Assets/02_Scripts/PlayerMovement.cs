using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform feetPos;
    private float checkRadius = .2f;
    private Collider2D[] steppingColl;
    private Collider2D stompingColl;
    [SerializeField] private LayerMask steppableObject;

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
        Move(Input.GetAxisRaw("Horizontal"));
        Jump(Input.GetKeyDown(KeyCode.Space));
        //AnimateDeath();
    }

    public void Move(float inputH)
    {
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

    public void Jump(bool inputV)
    {
        steppingColl = Physics2D.OverlapCircleAll(
            feetPos.position,
            checkRadius,
            steppableObject
            );
        foreach(Collider2D collider in steppingColl)
        {
            bool onGround = collider.gameObject.CompareTag("GROUND");
            if (onGround && inputV)
                rb.velocity = Vector2.up * jumpForce;
            bool onPlayer = collider.gameObject.CompareTag("ENEMY");
            if (onPlayer && !onGround)
            {
                rb.velocity = Vector2.up * bounceForce;
            }
            AnimateVertical(onGround);
        }
    }

    private void AnimateVertical(bool isStepping)
    { 
        animator.SetBool("isStepping", isStepping);
    }

    /*
    public void AnimateDeath()
    {
        stompingColl = Physics2D.OverlapCircleAll(
            headPos.position,
            checkRadius,
            steppableObject
            );
        if(stompingColl != null)
        {
            Debug.Log("dead");
        }
    }
    */
}
