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

    // Start is called before the first frame update
    void Start()
    {
        metroSource = GetComponent<AudioSource>();
        secPerBeat = 60f / BPM;
        metroSource.Play();
        if (noteAccuracy == 0)
        {
            noteAccuracy = .5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!metroSource.isPlaying)
        {
            metroSource.Play();
        }
        metroPosition = metroSource.time;
        //add 1 so the first beat calculates to 1, not 0
        metroPositionInBeats = metroPosition / secPerBeat + 1f - .2f;
    }

    public double GetBeatOffset()
    {
        double dec = metroPositionInBeats - Math.Floor(metroPositionInBeats);
        while(dec >= noteAccuracy)
        {
            dec -= noteAccuracy;
        }
        return dec;
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
