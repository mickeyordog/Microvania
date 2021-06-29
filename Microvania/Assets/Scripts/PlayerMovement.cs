using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float walkSpeed = 10f;

    public Transform groundCheck;
    public float groundedRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            anim.SetTrigger("jumpTrigger");
        }
        float horizontalVelocity = Input.GetAxisRaw("Horizontal");
        if (horizontalVelocity < 0f)
        {
            sr.flipX = true;
        } else if (horizontalVelocity > 0f)
        {
            sr.flipX = false;
        }
        rb.velocity = new Vector2(horizontalVelocity * walkSpeed, rb.velocity.y);

        // this block should be in fixedUpdate
        bool wasGrounded = isGrounded;
        isGrounded = false;
        anim.ResetTrigger("landedTrigger");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    Debug.Log("Landed");
                    anim.SetTrigger("landedTrigger");

                }
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }
}
