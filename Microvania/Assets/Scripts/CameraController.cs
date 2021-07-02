using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    public bool followPlayer = true;
    public bool followY = false;

    public static CameraController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);
    }
        // Start is called before the first frame update
        void Start()
    {
        player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            float targetY;
            if (followY)
            {
                targetY = player.transform.position.y;
            }
            else
            {
                targetY = transform.position.y;
            }
            transform.position = new Vector3(player.position.x, targetY, transform.position.z);
        }
        
    }
}
