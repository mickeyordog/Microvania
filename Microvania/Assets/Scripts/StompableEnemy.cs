using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompableEnemy : MonoBehaviour
{
    public Vector2 stompableDirection = Vector2.up;
    public float minDotWithStompDirection = 0.75f;
    public float deathLaunchForce = 10f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die(Vector2 launchDirection)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(launchDirection * deathLaunchForce);
        
    }

    // return true if stomp successful
    public bool TryToStomp(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        float dot = Vector2.Dot(stompableDirection, contact.normal);
        //Debug.Log(dot);
        if (dot >= minDotWithStompDirection)
        {
            //Die(contact.normal);
            return true;
        } else
        {
            return false;
        }
    }
}
