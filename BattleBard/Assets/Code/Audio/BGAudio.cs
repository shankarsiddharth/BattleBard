using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudio : MonoBehaviour
{
    public AK.Wwise.Event bgAudioEvent;
    private bool _isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        bgAudioEvent.Post(gameObject);
        _isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopBGAudio()
    {
        bgAudioEvent.Stop(gameObject);
        _isPlaying = false;
    }

    public bool IsPlaying()
    {
        return _isPlaying;
    }
}