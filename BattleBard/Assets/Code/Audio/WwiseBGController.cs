using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseBGController : MonoBehaviour
{
    private BGAudio _bgAudio;

    void Awake()
    {
        _bgAudio = gameObject.GetComponentInChildren<BGAudio>();
        if (_bgAudio == null)
        {
            throw new NullReferenceException("_bgAudio is null in WwiseBGController");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(OnDrumComboPlayed);
    }

    private void OnDrumComboPlayed(ComboBase arg0, int arg1, Vector3 arg2)
    {
        if (_bgAudio.IsPlaying())
        {
            _bgAudio.StopBGAudio();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
