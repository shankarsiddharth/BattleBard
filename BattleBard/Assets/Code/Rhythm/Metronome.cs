using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metronome : MonoBehaviour
{
    public AK.Wwise.Event metronomeClip;
    private uint playID = 0;
    bool isPlaying = false;

    public float noteAccuracy;
    public int BPM;
    private AudioSource metroSource;
    private float metroPosition;
    //private float metroPositionInBeats;
    private float secPerBeat;
    private float startTime = 0;
    private float fudgeOffset;
    private float lastPlayed = 0.0f;
    private float lastBeatPlayedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        metroSource = GetComponent<AudioSource>();
        secPerBeat = 60f / BPM;
        //metroSource.Play();
        //startTime = (float)AudioSettings.dspTime;
        //startTime = Time.time;
        if (noteAccuracy == 0)
        {
            noteAccuracy = .5f;
        }
        fudgeOffset = 0f;
        lastPlayed = 0.0f;
        //metronomeClip.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        /*//metroPosition = (float)AudioSettings.dspTime;
        metroPosition = (float)Time.time;
        //add 1 so the first beat calculates to 1, not 0
        metroPositionInBeats = (metroPosition - startTime) / secPerBeat + 1f;
        /*if (/*!metroSource.isPlaying &&#2# metroPositionInBeats - lastPlayed >= secPerBeat)
        {
            lastPlayed = metroPositionInBeats;
            Debug.Log("START Tick");
            metroSource.Play();
            Debug.Log("END Tick");
        }#1#

        /*if(!isPlaying && metroPositionInBeats - (float)Math.Floor(metroPositionInBeats) < .1)
        {
            Debug.Log("START Tick");
            playID = metronomeClip.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, AKCallbackFunction);
            Debug.Log("END Tick");
            isPlaying = true;           
        }#1#

        if (metroPositionInBeats - lastPlayed >= secPerBeat)
        {
            lastPlayed = metroPositionInBeats;
            //Debug.Log("START Tick");
            //playID = metronomeClip.Post(gameObject/*, (uint)AkCallbackType.AK_EndOfEvent, AKCallbackFunction#1#);
            //Debug.Log("END Tick");
            //isPlaying = true;
        }*/
    }

    private void AKCallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if(in_type == AkCallbackType.AK_EndOfEvent)
        {
            isPlaying = false;
            metronomeClip.Stop(gameObject);
        }
    }

    public double GetBeatOffset()
    {
        float closestBeat = GetClosestBeat();
        //return metroPositionInBeats - closestBeat + fudgeOffset;
        return (Time.time - startTime) - closestBeat + fudgeOffset;
    }

    public int GetLastBeatCount()
    {
        //return (int) Math.Floor(metroPositionInBeats);
        return (int) Math.Floor(lastBeatPlayedTime - startTime);
    }

    public float GetClosestBeat()
    {
        //int numSubdivisions = (int)Math.Round(metroPositionInBeats / noteAccuracy);
        int numSubdivisions = (int)Math.Round((Time.time - startTime) / noteAccuracy);
        return (float)(numSubdivisions * noteAccuracy);
    }

    public void PlayBeat()
    {
        lastBeatPlayedTime = Time.time;
        if (startTime == 0)
        {
            startTime = lastBeatPlayedTime;
        }
        //Debug.Log("START Tick");
        metronomeClip.Post(gameObject);
        //Debug.Log("START Tick");
    }
}
