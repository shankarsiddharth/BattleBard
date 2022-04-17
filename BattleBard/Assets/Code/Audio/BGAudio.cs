using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudio : MonoBehaviour
{
    public AK.Wwise.Event bgAudioEvent;

    // Start is called before the first frame update
    void Start()
    {
        bgAudioEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopBGAudio()
    {
        bgAudioEvent.Stop(gameObject);
    }
}