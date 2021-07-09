using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompableEnemy : Enemy
{
    public Vector2 stompableDirection = Vector2.up;
    public float minDotWithStompDirection = 0.75f;
    public float deathLaunchForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    // return true if stomp successful
    public bool TryToStomp(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        float dot = Vector2.Dot(stompableDirection, contact.normal);
        //Debug.Log(dot);
        if (dot >= minDotWithStompDirection)
        {
            bool shouldGoLeft = contact.normal.x > 0f ? true : false;
            Die(shouldGoLeft);
            return true;
        } else
        {
            return false;
        }
    }
}

