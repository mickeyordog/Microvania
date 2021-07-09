using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float walkSpeed = 10f;
    public float dashSpeed = 10f;
    public float terminalVelocity = 10f;

    private Vector2 velocity = Vector2.zero;
    public float movementSmoothing = 0.05f;

    public Transform groundCheck;
    public float groundedRadius;
    public LayerMask whatIsGround;
    private float timeLastOnGround = 0f;
    public float coyoteTimeInterval = 0.1f;
    private float timeLastPressedJump = Mathf.NegativeInfinity;
    private bool jumpJustPressed = false;
    public float jumpBufferInterval = 0.1f;
    private float timeOfLastJump = Mathf.NegativeInfinity;
    public float variableJumpHeightInterval = 0.25f;

    public Transform wallCheck;
    public float walledRadius;
    private bool isOnWall;

    public Item rightMovementItem;
    public Item leftMovementItem;
    public Item jumpItem;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Collider2D coll;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
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
            jumpJustPressed = true;
        }
        //if ((isGrounded || isOnWall || Time.time - timeLastOnGround <= coyoteTimeInterval) && Time.time - timeLastPressedJump <= jumpBufferInterval)
        /*if (CanJump())
        {
            Jump();
        }*/
        if (Input.GetButtonDown("Fire1"))
        {
            //Dash();

        }
        else
        {
            //SetHorizontalVelocity();
        }

        if (Input.GetAxisRaw("Vertical") < 0f && PlatformIsBelow())
        {
            StartCoroutine(nameof(DisableCollider));
        }


        // this method should be in fixedUpdate
        CheckGrounded();

        CheckOnWall();
    }

    private bool PlatformIsBelow()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position + Vector3.down / 4, 1 << LayerMask.NameToLayer("Platform"));
        return hit;
    }

    private void SetHorizontalVelocity()
    {
        float targetHorizontalVelocity = Input.GetAxisRaw("Horizontal") * walkSpeed;
        if ((targetHorizontalVelocity < 0f && !PlayerInventory.Instance.ContainsItem(leftMovementItem)) || 
            (targetHorizontalVelocity > 0f && !PlayerInventory.Instance.ContainsItem(rightMovementItem)))
        {
            targetHorizontalVelocity = 0f;
        }
        if (targetHorizontalVelocity < 0f)
        {
            transform.rotation = Quaternion.Euler(transform.position.x, 180, transform.position.z);
        }
        else if (targetHorizontalVelocity > 0f)
        {
            transform.rotation = Quaternion.Euler(transform.position.x, 0, transform.position.z);
        }

        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(targetHorizontalVelocity, rb.velocity.y), ref velocity, movementSmoothing);
    }

    private bool CanJump()
    {
        return ((isGrounded && Time.time - timeLastPressedJump <= jumpBufferInterval) || ((isOnWall || Time.time - timeLastOnGround <= coyoteTimeInterval) && jumpJustPressed))
            && PlayerInventory.Instance.ContainsItem(jumpItem);
        //return ((isGrounded && Time.time - timeLastPressedJump <= jumpBufferInterval) || ((isOnWall || Time.time - timeLastOnGround <= coyoteTimeInterval) && Input.GetButtonDown("Jump")))
        //    && PlayerInventory.Instance.ContainsItem(jumpItem);
    }

    private void Dash()
    {
        rb.velocity = new Vector2(transform.right.x * dashSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        timeLastPressedJump = Mathf.NegativeInfinity;
        timeOfLastJump = Time.time;
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        isGrounded = false;
        anim.SetTrigger("jumpTrigger");
        AudioManager.Instance.PlaySound("Jump");
    }

    private IEnumerator DisableCollider()
    {
        coll.isTrigger = true;
        yield return new WaitForSeconds(0.4f);
        coll.isTrigger = false;
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
                    //Debug.Log("Landed on wall");
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
                    //Debug.Log("Landed");
                    anim.SetTrigger("landedTrigger");

                }
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && rb.velocity.y > 0f && Time.time - timeOfLastJump <= variableJumpHeightInterval)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        if (rb.velocity.y < -terminalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -terminalVelocity);
        }

        SetHorizontalVelocity();
        if (CanJump())
        {
            Jump();
        }
        jumpJustPressed = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
        Gizmos.DrawWireSphere(wallCheck.position, walledRadius);
    }
}
