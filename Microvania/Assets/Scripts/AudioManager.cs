using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);
    }

    public List<NewSound> sounds;

    private List<AudioSource> audioSources;

    private void Start()
    {
        audioSources = new List<AudioSource>();

        foreach (var sound in sounds)
        {
            var newSource = this.gameObject.AddComponent<AudioSource>();
            newSource.clip = sound.clip;
            newSource.volume = sound.volume;
            newSource.loop = sound.loop;

            audioSources.Add(newSource);
        }
    }

    public void PlaySound(string nameOfClipToPlay)
    {
        var selectedClip = audioSources.Where(c => c.clip.name == nameOfClipToPlay).FirstOrDefault();

        if (selectedClip == null)
            return;

        selectedClip.Play();
    }
}

[System.Serializable]
public struct NewSound
{
    public AudioClip clip;
    public float volume;
    public bool loop;
}

