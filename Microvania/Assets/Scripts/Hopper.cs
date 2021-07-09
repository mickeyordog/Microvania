using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    private Rigidbody2D rb;
    public float hopForce = 10f;
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
        InvokeRepeating(nameof(Hop), 3f, 3f);
    }

    void Hop()
    {
        Vector2 forceToAdd = (transform.right + transform.up).normalized * hopForce;
        Debug.Log(forceToAdd);
        rb.AddForce(transform.right, ForceMode2D.Impulse);
    }
}
