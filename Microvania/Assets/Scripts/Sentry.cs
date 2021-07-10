using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Sentry : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public LookMode lookMode = LookMode.Rotate;
    public Transform lookDir;
    Player player;
    public Color idleColor;
    public Color targetedColor;
    public float viewRadius = 5f;
    public float viewAngle = 50f;
    public float viewAngleTargetedFactor = 1 / 5f;
    Light2D viewLight;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        viewLight = GetComponentInChildren<Light2D>();
        //viewLight.pointLightInnerRadius = viewRadius;
        viewLight.pointLightOuterRadius = viewRadius;
        viewLight.pointLightOuterAngle = viewAngle;
        viewLight.color = idleColor;
    }

    // Update is called once per frame
    void Update()
    {
        switch (lookMode)
        {
            case LookMode.Rotate:
                Rotate();
                SearchForPlayer();

                break;
            case LookMode.AtPlayer:
                LookAtPlayer();
                break;
        }

        
    }


    private void SearchForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir.position - transform.position, viewRadius);
        if (hit && hit.transform.GetComponent<Player>())
        {
            lookMode = LookMode.AtPlayer;
            viewLight.pointLightOuterAngle = viewAngle * viewAngleTargetedFactor;
            viewLight.color = targetedColor;
        }
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
    }

    private void LookAtPlayer()
    {
        Vector3 playerPos = player.transform.position;
        float angleToPlayer = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToPlayer);
    }
}

[SerializeField]
public enum LookMode
{
    AtPlayer, Rotate
}