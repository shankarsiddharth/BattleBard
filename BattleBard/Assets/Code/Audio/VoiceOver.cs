using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = AK.Wwise.Event;

[Serializable]
public enum TVoiceOverAudioType
{
    kIntro,
    kReinforcedGate,
    kTrapHeal,
    kTakeBackOurHome,
    kTheyreRetreating,
    kThatOneThere,
    kTheVillageisOursAgain
}


[Serializable]
public struct VoiceOverData
{
    public TVoiceOverAudioType voiceOverAudioType;
    public AK.Wwise.Event voiceOverEvent;
}

public class VoiceOver : MonoBehaviour
{
    [SerializeField] public List<VoiceOverData> voiceOverDataList = new List<VoiceOverData>();

    private GameObject uiGameObject;
    private TVoiceOverAudioType _currentVoiceOverAudioType;

    void Awake()
    {
        uiGameObject = GameObject.FindGameObjectWithTag("UI");
        if (uiGameObject == null)
        {
            throw new NullReferenceException("uiGameObject is null in VoiceOver");
        }

        WwiseAudioVolumeController.SetWwiseAudioVolume();
        WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kSFX);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayVoiceOver(TVoiceOverAudioType voiceOverAudioType)
    {
        _currentVoiceOverAudioType = voiceOverAudioType;

        if (voiceOverAudioType == TVoiceOverAudioType.kIntro
            //|| voiceOverAudioType == TVoiceOverAudioType.kReinforcedGate
            || voiceOverAudioType == TVoiceOverAudioType.kTheVillageisOursAgain)
        {

            WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kSFX);
            WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kMetronome);
            uiGameObject.SetActive(false);

            /*WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(WwiseAudioVolumeController.CurrentVolume, TAudioBusType.kSFX);
            */

        }

        foreach (VoiceOverData voiceOverData in voiceOverDataList)
        {
            if (voiceOverData.voiceOverAudioType == voiceOverAudioType)
            {
                voiceOverData.voiceOverEvent.Post(gameObject, (uint) AkCallbackType.AK_EndOfEvent, AKCallbackFunction);
            }
        }
    }

    public void StopVoiceOver(TVoiceOverAudioType voiceOverAudioType)
    {
        foreach (VoiceOverData voiceOverData in voiceOverDataList)
        {
            if (voiceOverData.voiceOverAudioType == voiceOverAudioType)
            {
                WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(WwiseAudioVolumeController.CurrentVolume,
                    TAudioBusType.kSFX);
                WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(WwiseAudioVolumeController.CurrentVolume,
                    TAudioBusType.kMetronome);
                voiceOverData.voiceOverEvent.Stop(gameObject);
            }
        }
    }

    private void AKCallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if (!(/*_currentVoiceOverAudioType == TVoiceOverAudioType.kIntro
            ||*/ _currentVoiceOverAudioType == TVoiceOverAudioType.kTheVillageisOursAgain))
        {
            uiGameObject.SetActive(true);
            if (in_type == AkCallbackType.AK_EndOfEvent)
            {
                WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(WwiseAudioVolumeController.CurrentVolume,
                    TAudioBusType.kSFX);
                WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(WwiseAudioVolumeController.CurrentVolume,
                    TAudioBusType.kMetronome);
            }
        }
    }
}