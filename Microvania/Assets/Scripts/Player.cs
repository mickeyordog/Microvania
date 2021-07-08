using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : Mob
{
    Vector3 respawnPoint;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Collider2D coll;

    
    public PlayerMovement movement;

    public static Player Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);

        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        movement = GetComponent<PlayerMovement>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DieWrapper(bool shouldGoLeft)
    {
        StartCoroutine(nameof(Die), shouldGoLeft);
    }

    IEnumerator Die(bool shouldGoLeft)
    {
        
        movement.enabled = false;
        CameraController.Instance.followPlayer = false;
        SpinAway(shouldGoLeft);

        yield return new WaitForSeconds(2f);


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup)
        {
            pickup.PickUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactDamager cd = collision.gameObject.GetComponent<ContactDamager>();
        if (cd)
        {
            DieWrapper(true);
        }

        StompableEnemy se = collision.gameObject.GetComponent<StompableEnemy>();
        if (se)
        {
            if (se.TryToStomp(collision))
            {
                movement.Jump();
            } else
            {
                bool shouldGoLeft = collision.GetContact(0).normal.x < 0 ? true : false;
                DieWrapper(shouldGoLeft);
            }
        }

        PatrollingPlatform pp = collision.gameObject.GetComponent<PatrollingPlatform>();
        if (pp)
        {
            transform.parent = pp.transform;
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PatrollingPlatform pp = collision.gameObject.GetComponent<PatrollingPlatform>();
        if (pp)
        {
            transform.parent = null;
        }
    }
}
