using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    public bool followY = false;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float targetY;
        if (followY)
        {
            targetY = player.transform.position.y;
        } else
        {
            targetY = transform.position.y;
        }
        transform.position = new Vector3(player.position.x, targetY, transform.position.z);
    }
}
