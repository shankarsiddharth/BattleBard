using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Metronome : MonoBehaviour
{
    public AK.Wwise.Event metronomeClip;

    [SerializeField]
    private float _noteAccuracy;
    private float _fudgeOffset;
    public int BPM;
    private float _secPerBeat;
    private float _startTime = 0.0f;
    private float _lastBeatPlayedTime = 0.0f;
    private float _closestBeatTime = 0.0f;
    private int _lastBeatCount = 0;
    private int _closestBeatCount = 0;
    private float _beatOffset = 0.0f;
    private bool _isPlaying = false;

    void Awake()
    {
        _secPerBeat = 60f / BPM;
        if (_noteAccuracy == 0)
        {
            _noteAccuracy = 0.25f;
        }
        _fudgeOffset = _noteAccuracy;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AKCallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if(in_type == AkCallbackType.AK_EndOfEvent)
        {
            _isPlaying = false;
            metronomeClip.Stop(gameObject);
        }
    }

    public double GetBeatOffset()
    {
        CalculateClosestBeat();
        return _beatOffset;
    }

    public float GetFudgeOffset()
    {
        return _fudgeOffset;
    }

    public float GetNoteAccuracy()
    {
        return _fudgeOffset;
    }

    public int GetLastBeatCount()
    {
        _lastBeatCount = (int) Math.Floor(_lastBeatPlayedTime - _startTime);
        return _lastBeatCount;
    }

    public float GetLastBeatTime()
    {
        return _lastBeatPlayedTime;
    }

    private float CalculateClosestBeatTime()
    {
        float tolerance = 0.00000001f;

        float currentTime = Time.time;
        float previousBeatTime = _lastBeatPlayedTime - _secPerBeat;
        float currentBeatTime = _lastBeatPlayedTime;
        float nextBeatTime = _lastBeatPlayedTime + _secPerBeat;

        float differencePreviousBeat = Math.Abs(currentTime - previousBeatTime);
        float differenceCurrentBeat = Math.Abs(currentTime - currentBeatTime);
        float differenceNextBeat = Math.Abs(currentTime - nextBeatTime);

        float minimumValue = new[]{differencePreviousBeat, differenceCurrentBeat, differenceNextBeat}.Min();

        _closestBeatTime = currentBeatTime;
        if (Math.Abs(minimumValue - differencePreviousBeat) < tolerance)
        {
            _closestBeatTime = previousBeatTime;
            _beatOffset = differencePreviousBeat;
        }
        else if(Math.Abs(minimumValue - differenceCurrentBeat) < tolerance)
        {
            _closestBeatTime = currentBeatTime;
            _beatOffset = differenceCurrentBeat;
        }
        else if (Math.Abs(minimumValue - differenceNextBeat) < tolerance)
        {
            _closestBeatTime = nextBeatTime;
            _beatOffset = differenceNextBeat;
        }

        _closestBeatTime -= _startTime;
        return _closestBeatTime;
    }

    public int CalculateAndGetClosestBeat()
    {
        float closestBeatTime = CalculateAndGetClosestBeatTime();

        closestBeatTime -= _startTime;
        int closestBeat = (int) Math.Floor(closestBeatTime);
        return closestBeat;
    }

    public float CalculateAndGetClosestBeatTime()
    {
        float tolerance = 0.00000001f;

        float currentTime = Time.time;
        float previousBeatTime = _lastBeatPlayedTime - _secPerBeat;
        float currentBeatTime = _lastBeatPlayedTime;
        float nextBeatTime = _lastBeatPlayedTime + _secPerBeat;

        float differencePreviousBeat = Math.Abs(currentTime - previousBeatTime);
        float differenceCurrentBeat = Math.Abs(currentTime - currentBeatTime);
        float differenceNextBeat = Math.Abs(currentTime - nextBeatTime);

        float minimumValue = new[] { differencePreviousBeat, differenceCurrentBeat, differenceNextBeat }.Min();

        float closestBeatTime = currentBeatTime;
        if (Math.Abs(minimumValue - differencePreviousBeat) < tolerance)
        {
            closestBeatTime = previousBeatTime;
        }
        else if (Math.Abs(minimumValue - differenceCurrentBeat) < tolerance)
        {
            closestBeatTime = currentBeatTime;
        }
        else if (Math.Abs(minimumValue - differenceNextBeat) < tolerance)
        {
            closestBeatTime = nextBeatTime;
        }

        //closestBeatTime -= _startTime;
        return closestBeatTime;
    }

    private int CalculateClosestBeat()
    {
        _closestBeatCount = (int)Math.Floor(CalculateClosestBeatTime());
        return _closestBeatCount;
    }

    public float GetClosestBeatTime()
    {
        return _closestBeatTime;
    }

    public int GetClosestBeat()
    {
        return _closestBeatCount;
    }

    public void PlayBeat()
    {
        //Debug.Log("START Tick");
        metronomeClip.Post(gameObject);
        //Debug.Log("START Tick");
        _lastBeatPlayedTime = Time.time;
        if (_startTime == 0)
        {
            _startTime = _lastBeatPlayedTime;
        }
    }

    public float GetSecPerBeat()
    {
        return _secPerBeat;
    }
}
