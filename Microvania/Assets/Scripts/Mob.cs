using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public static float deathVel = 10f; //how fast should fly away on death
    public static float deathSpin = 1000f; //how much torque to apply on death

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SpinAway(bool shouldGoLeft)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D coll = GetComponent<Collider2D>();
        coll.enabled = false;
        rb.velocity = new Vector2((shouldGoLeft) ? -1f : 1f, 1f) * deathVel;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque((shouldGoLeft) ? deathSpin : -deathSpin);
    }
}
