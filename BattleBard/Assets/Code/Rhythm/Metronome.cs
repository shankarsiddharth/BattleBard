using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metronome : MonoBehaviour
{
    public int BPM;
    private AudioSource metroSource;
    private float metroPosition;
    private float metroPositionInBeats;
    private float dspMetroTime;
    private float secPerBeat;

    // Start is called before the first frame update
    void Start()
    {
        metroSource = GetComponent<AudioSource>();
        dspMetroTime = (float)AudioSettings.dspTime;
        secPerBeat = 60f / BPM;
        metroSource.Play();
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
        //Debug.Log("SPB:");
        //Debug.Log(secPerBeat);
        //Debug.Log("Position");
        //Debug.Log(metroPosition);
        //Debug.Log("Beats:");
        //Debug.Log(metroPositionInBeats);
        if (metroPositionInBeats - Math.Floor(metroPositionInBeats) <= .00001 && Math.Floor(metroPositionInBeats) % 4 == 1)
        {
            //do thing here on beat 1
        }
        //Debug.Log(metroPositionInBeats - Math.Floor(metroPositionInBeats));
    }

    public double GetBeatOffset()
    {
        double dec = metroPositionInBeats - Math.Floor(metroPositionInBeats);
        if(dec < .5f)
        {
            return dec;
        }
        else if(dec < 1f)
        {
            return 1 - dec;
        }
        else
        {
            return -1;
        }
    }
}
