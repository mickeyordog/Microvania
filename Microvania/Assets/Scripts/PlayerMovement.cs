using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float walkSpeed = 10f;
    public float dashSpeed = 10f;

    private Vector2 velocity = Vector2.zero;
    public float movementSmoothing = 0.05f;

    public Transform groundCheck;
    public float groundedRadius;
    public LayerMask whatIsGround;
    private float timeLastOnGround = 0f;
    public float coyoteTimeInterval = 0.1f;
    private float timeLastPressedJump = Mathf.NegativeInfinity;
    public float jumpBufferInterval = 0.1f;

    public Transform wallCheck;
    public float walledRadius;
    private bool isOnWall;



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
        if (Input.GetButtonDown("Jump"))
        {
            timeLastPressedJump = Time.time;
        }
        //if ((isGrounded || isOnWall || Time.time - timeLastOnGround <= coyoteTimeInterval) && Time.time - timeLastPressedJump <= jumpBufferInterval)
        if ((isGrounded && Time.time - timeLastPressedJump <= jumpBufferInterval) || ((isOnWall || Time.time - timeLastOnGround <= coyoteTimeInterval) && Input.GetButtonDown("Jump")))
        {
            timeLastPressedJump = Mathf.NegativeInfinity;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isGrounded = false;
            anim.SetTrigger("jumpTrigger");
            AudioManager.Instance.PlaySound("Jump");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Dashing");
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashSpeed, rb.velocity.y);
            rb.velocity = new Vector2(transform.right.x * dashSpeed, rb.velocity.y);

        } else
        {
            
            float targetHorizontalVelocity = Input.GetAxisRaw("Horizontal") * walkSpeed;
            if (targetHorizontalVelocity < 0f)
            {
                //sr.flipX = true;
                transform.rotation = Quaternion.Euler(transform.position.x, 180, transform.position.z);
            }
            else if (targetHorizontalVelocity > 0f)
            {
                //sr.flipX = false;
                transform.rotation = Quaternion.Euler(transform.position.x, 0, transform.position.z);
            }


            //rb.velocity = new Vector2(targetHorizontalVelocity, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(targetHorizontalVelocity, rb.velocity.y), ref velocity, movementSmoothing);
        }
        

        // this method should be in fixedUpdate
        CheckGrounded();

        CheckOnWall();
    }

    private void CheckOnWall()
    {
        bool wasOnWall = isOnWall;
        isOnWall = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, walledRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isOnWall = true;
                if (!wasOnWall)
                {
                    Debug.Log("Landed on wall");
                }
                break;
            }
        }
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        anim.ResetTrigger("landedTrigger");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                timeLastOnGround = Time.time;
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
        Gizmos.DrawWireSphere(wallCheck.position, walledRadius);
    }
}
