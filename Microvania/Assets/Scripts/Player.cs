using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    Vector3 respawnPoint;

    private Rigidbody2D rb;
    private Collider2D coll;
    public float deathVel = 10f; //how fast should fly away on death
    public float deathSpin = 1000f; //how much torque to apply on death
    PlayerMovement movement;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die(bool shouldGoLeft)
    {
        coll.enabled = false;
        rb.velocity = new Vector2((shouldGoLeft) ? -1f : 1f, 1f) * deathVel;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque((shouldGoLeft) ? deathSpin : -deathSpin);
        movement.enabled = false;
        CameraController.Instance.followPlayer = false;
        yield return new WaitForSeconds(2f);


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactDamager cd = collision.gameObject.GetComponent<ContactDamager>();
        if (cd)
        {
            StartCoroutine("Die", true);
        }

        StompableEnemy se = collision.gameObject.GetComponent<StompableEnemy>();
        if (se)
        {
            if (se.TryToStomp(collision))
            {
                movement.Jump();
            }
        }
    }
}
