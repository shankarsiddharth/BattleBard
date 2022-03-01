using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metronome : MonoBehaviour
{
    private float noteAccuracy;
    public int BPM;
    private AudioSource metroSource;
    private float metroPosition;
    private float metroPositionInBeats;
    private float secPerBeat;
    private float startTime;
    private float fudgeOffset;


    // Start is called before the first frame update
    void Start()
    {
        metroSource = GetComponent<AudioSource>();
        secPerBeat = 60f / BPM;
        //metroSource.Play();
        startTime = (float)AudioSettings.dspTime;
        if (noteAccuracy == 0)
        {
            noteAccuracy = .5f;
        }
        fudgeOffset = -.15f;
    }

    // Update is called once per frame
    void Update()
    {
        metroPosition = (float)AudioSettings.dspTime;
        //add 1 so the first beat calculates to 1, not 0
        metroPositionInBeats = (metroPosition - startTime) / secPerBeat + 1f;
        if (!metroSource.isPlaying && metroPositionInBeats - (float)Math.Floor(metroPositionInBeats) < .1)
        {
            metroSource.Play();
        }
    }

    public double GetBeatOffset()
    {
        float closestBeat = GetClosestBeat();
        return metroPositionInBeats - closestBeat + fudgeOffset;
    }

    public int GetLastBeatCount()
    {
        return (int) Math.Floor(metroPositionInBeats);
    }

    public float GetClosestBeat()
    {
        int numSubdivisions = (int)Math.Round(metroPositionInBeats / noteAccuracy);
        return (float)(numSubdivisions * noteAccuracy);
    }
}
