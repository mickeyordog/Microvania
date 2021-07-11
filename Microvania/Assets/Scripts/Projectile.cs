using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float distanceBeforeDestroy = 10f;
    [HideInInspector]
    public float maxSpawnLiveTime = 15f;
    float timeSpawned;
    Vector3 spawnPos;
    float squareDistanceBeforeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        timeSpawned = Time.time;
        spawnPos = transform.position;
        squareDistanceBeforeDestroy = Mathf.Pow(distanceBeforeDestroy, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: add to pool instead
        if (Time.time - timeSpawned >= maxSpawnLiveTime || (spawnPos - transform.position).sqrMagnitude >= squareDistanceBeforeDestroy)
            GetComponent<Enemy>().Die(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<Enemy>().Die(true);
    }
}
