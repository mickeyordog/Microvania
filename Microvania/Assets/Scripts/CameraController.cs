using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    Transform player;
    public bool followPlayer = true;
    public bool followY = false;

    [SerializeField]
    CinemachineVirtualCamera cinemachine;
    public float shakeIntensity = 2.5f;
    public float shakeTime = 0.1f;

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

    public IEnumerator ShakeCamera()
    {
        var shaker = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shaker.m_AmplitudeGain = shakeIntensity;
        yield return new WaitForSeconds(shakeTime);
        shaker.m_AmplitudeGain = 0f;
    }
}
