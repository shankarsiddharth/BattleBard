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
        //startTime = (float)AudioSettings.dspTime;
        startTime = Time.time;
        if (noteAccuracy == 0)
        {
            noteAccuracy = .5f;
        }
        fudgeOffset = 0f;
        WwiseAudioVolumeController.SetWwiseAudioVolume();
    }

    // Update is called once per frame
    void Update()
    {
        //metroPosition = (float)AudioSettings.dspTime;
        metroPosition = (float)Time.time;
        //add 1 so the first beat calculates to 1, not 0
        metroPositionInBeats = (metroPosition - startTime) / secPerBeat + 1f;
        if (!metroSource.isPlaying && metroPositionInBeats - (float)Math.Floor(metroPositionInBeats) < .1)
        {
            //metroSource.Play();            
        }

        if(!isPlaying && metroPositionInBeats - (float)Math.Floor(metroPositionInBeats) < .1)
        {
            playID = metronomeClip.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, AKCallbackFunction);
            isPlaying = true;           
        }
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
