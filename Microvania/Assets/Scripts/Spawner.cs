using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject objectToSpawn;
    public Vector2 pushDirection;
    public float pushSpeed = 5f;
    public float timeBeforeSpawn = 3f;
    public float secondsPerSpawn = 3f;
    public SpawnMode spawnMode = SpawnMode.Manual;
    public float distanceBeforeDestroy = 10f;
    public float maxSpawnLiveTime = 15f;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (spawnMode == SpawnMode.Animation)
            InvokeRepeating(nameof(BeginSpawnAnimation), timeBeforeSpawn, secondsPerSpawn);
        else if (spawnMode == SpawnMode.Manual)
            InvokeRepeating(nameof(SpawnObject), timeBeforeSpawn, secondsPerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginSpawnAnimation()
    {
        anim.SetTrigger("shouldFire");
    }

    // called in animator for SpawnMode.Animation
    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = pushDirection * pushSpeed;
        Projectile proj = spawnedObject.GetComponent<Projectile>();
        proj.distanceBeforeDestroy = distanceBeforeDestroy;
        proj.maxSpawnLiveTime = maxSpawnLiveTime;
    }
}

public enum SpawnMode
{
    Animation, Manual
}