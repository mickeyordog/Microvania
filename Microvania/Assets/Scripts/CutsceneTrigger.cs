using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    TimelinePlayer timelinePlayer;
    public PlayableAsset cutscene;
    public bool hasBeenPlayed = false;
    private void Start()
    {
        timelinePlayer = GetComponentInParent<TimelinePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenPlayed)
            timelinePlayer.StartCutscene(cutscene);
        hasBeenPlayed = true;
    }
}
