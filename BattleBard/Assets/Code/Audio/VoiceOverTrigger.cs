using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
    public TVoiceOverAudioType voiceOverAudioType;
    public GameObject triggerObject = null;

    private GameObject wwiseGameObject;
    private VoiceOver voiceOver;

    void Awake()
    {
        if (triggerObject == null)
        {
            throw new NullReferenceException("triggerObject is null in VoiceOverTrigger");
        }

        wwiseGameObject = GameObject.FindGameObjectWithTag("Wwise");
        if (wwiseGameObject == null)
        {
            throw new NullReferenceException("wwiseGameObject is null in VoiceOverTrigger");
        }

        voiceOver = wwiseGameObject.GetComponentInChildren<VoiceOver>();
    }

    private void OnTriggerEnter(Collider in_other)
    {
        if (triggerObject != null && triggerObject == in_other.gameObject)
        {
            voiceOver.PlayVoiceOver(voiceOverAudioType);
        }
    }
}