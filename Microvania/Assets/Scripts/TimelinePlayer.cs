using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class TimelinePlayer : MonoBehaviour
{
    PlayableDirector director;
    public GameObject controlPanel;
    Player player;
    Vector3 playerSpriteOffset;
    float gravityScale;
    CinemachineBrain cm;
    
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Start()
    {
        player = Player.Instance;
        playerSpriteOffset = player.sr.transform.localPosition;
        cm = CameraController.Instance.GetComponent<CinemachineBrain>();
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        Debug.Log("stopped");
        player.movement.enabled = true;
        player.rb.gravityScale = gravityScale;
        player.transform.position += player.sr.transform.localPosition - playerSpriteOffset;
        player.sr.transform.localPosition = playerSpriteOffset;
        cm.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
    }
    private void Director_Played(PlayableDirector obj)
    {
        Debug.Log("started");
        player.movement.enabled = false;
        gravityScale = player.rb.gravityScale;
        player.rb.gravityScale = 0f;
        player.rb.velocity = Vector2.zero;
        cm.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
    }

    public void StartCutscene(PlayableAsset cutscene)
    {
        director.playableAsset = cutscene;
        director.Play();
    }
}