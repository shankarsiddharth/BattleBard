using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum TAudioBusType
{
    kAll,
    kMaster,
    kMusic,
    kSFX,
    kVoiceOver
}

public static class WwiseAudioVolumeController
{
    public static int CurrentVolume = 50;

    public static void SetWwiseAudioVolume(int volume)
    {
        CurrentVolume = volume;
        AkSoundEngine.SetRTPCValue("MasterVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MusicVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("SFXVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("VOVolume", CurrentVolume);
    }

    public static void SetWwiseAudioVolume()
    {
        AkSoundEngine.SetRTPCValue("MasterVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MusicVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("SFXVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("VOVolume", CurrentVolume);
    }

    public static void SetWwiseAudioVolumeForAudioBusType(int volume, TAudioBusType audioBusType = TAudioBusType.kAll)
    {
        switch (audioBusType)
        {
            case TAudioBusType.kMaster:
            {
                AkSoundEngine.SetRTPCValue("MasterVolume", volume);
            }
                break;
            case TAudioBusType.kMusic:
            {
                AkSoundEngine.SetRTPCValue("MusicVolume", volume);
            }
                break;
            case TAudioBusType.kSFX:
            {
                AkSoundEngine.SetRTPCValue("SFXVolume", volume);
            }
                break;
            case TAudioBusType.kVoiceOver:
            {
                AkSoundEngine.SetRTPCValue("VOVolume", volume);
            }
                break;
            default:
            case TAudioBusType.kAll:
            {
                AkSoundEngine.SetRTPCValue("MasterVolume", volume);
                AkSoundEngine.SetRTPCValue("MusicVolume", volume);
                AkSoundEngine.SetRTPCValue("SFXVolume", volume);
                AkSoundEngine.SetRTPCValue("VOVolume", volume);
            }
                break;
        }
    }
}