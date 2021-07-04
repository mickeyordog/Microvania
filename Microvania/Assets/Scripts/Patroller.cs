using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField]
    List<Vector3> waypoints;
    int currentWaypointIndex = 0;
    public float speed = 5f;
    public bool shouldFlip = false;

    private void Awake()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i] = waypoints[i] + transform.position;
        }
        waypoints.Add(transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);
        if (transform.position == waypoints[currentWaypointIndex])
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            if (shouldFlip)
            {
                Vector3 directionToTarget = waypoints[currentWaypointIndex] - transform.position;
                if (directionToTarget.x < 0f)
                {
                    transform.rotation = Quaternion.Euler(transform.position.x, 180, transform.position.z);
                }
                else if (directionToTarget.x > 0f)
                {
                    transform.rotation = Quaternion.Euler(transform.position.x, 0, transform.position.z);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 pt in waypoints)
        {
            Gizmos.DrawWireSphere(transform.position + pt, 0.25f);
        }
    }
}
