using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DrumSound
{
    public Drums drum;
    public AudioClip audio;
}

public class SFXManager : MonoBehaviour
{
    [Tooltip("The gameobject that will house all of the AudioSource components for the SFX Manager")]
    public GameObject drumSoundSlave;

    [Space]
    [Tooltip("The background sound to play continuously")]
    public AudioClip bgSound;
    [Range(0f,1f)]
    public float bgSoundVolume;

    [Tooltip("List of drums and their AudioClips")]
    public List<DrumSound> drumsSFX;

    private Dictionary<Drums, List<AudioSource>> _drumSources;

    // Start is called before the first frame update
    void Start()
    {
        _drumSources = new Dictionary<Drums, List<AudioSource>>();

		//GameEvents.onDrumPlayed += OnDrumPlayed;

        foreach (DrumSound ds in drumsSFX)
		{
            List<AudioSource> srcs = new List<AudioSource>();
            AudioSource drumSource = drumSoundSlave.AddComponent<AudioSource>();
            srcs.Add(drumSource);
            drumSource.clip = ds.audio;
            _drumSources.Add(ds.drum, srcs);
		}

        if (bgSound != null)
        {
            // Add the bg music and play it
            AudioSource bgSource = drumSoundSlave.AddComponent<AudioSource>();
            bgSource.clip = bgSound;
            bgSource.volume = bgSoundVolume;
            bgSource.loop = true;
            bgSource.Play();
        }
    }

    void OnDrumPlayed(Drums drum)
	{
        bool played = false;

        // Find a source that is not playing currently
        foreach (AudioSource audioSource in _drumSources[drum])
		{
            if (audioSource.isPlaying)
                continue;

            audioSource.Play();
            played = true;
            break;
		}

        if (!played)
        {
            // If there are none, add a new source and play it
            AudioSource AS = drumSoundSlave.AddComponent<AudioSource>();
            foreach (DrumSound ds in drumsSFX)
                if (ds.drum == drum)
                {
                    AS.clip = ds.audio;
                    AS.Play();
                    break;
                }

            _drumSources[drum].Add(AS);
        }
	}
}
