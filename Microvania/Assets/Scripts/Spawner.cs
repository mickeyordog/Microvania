using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject objectToSpawn;
    public Vector2 pushDirection;
    public float pushSpeed = 5f;
    public float secondsPerSpawn = 3f;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating(nameof(BeginSpawnAnimation), 3f, secondsPerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginSpawnAnimation()
    {
        anim.SetTrigger("shouldFire");
    }

    // called in animator
    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = pushDirection * pushSpeed;
    }
}
