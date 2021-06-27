using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float walkSpeed = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
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
    }
}
