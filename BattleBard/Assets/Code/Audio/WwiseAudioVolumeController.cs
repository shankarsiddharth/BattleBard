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
    kMetronome,
    kVoiceOver
}

public static class WwiseAudioVolumeController
{
    public static int DeltaVolume = 5;
    public static int MinimumVolume = 0;
    public static int MaximumVolume = 100;
    public static int CurrentVolume = 50;

    public static void IncreaseVolume()
    {
        int newVolume = CurrentVolume + DeltaVolume;
        if (newVolume >= MaximumVolume)
        {
            newVolume = MaximumVolume;
        }
        SetWwiseAudioVolume(newVolume);
    }


    public static void DecreaseVolume()
    {
        int newVolume = CurrentVolume - DeltaVolume;
        if (newVolume <= MinimumVolume)
        {
            newVolume = MinimumVolume;
        }
        SetWwiseAudioVolume(newVolume);
    }

    public static void SetWwiseAudioVolume(int volume)
    {
        CurrentVolume = volume;
        AkSoundEngine.SetRTPCValue("MasterVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MusicVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("SFXVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("VOVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MetronomeVolume", CurrentVolume);
    }

    public static void SetWwiseAudioVolume()
    {
        AkSoundEngine.SetRTPCValue("MasterVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MusicVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("SFXVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("VOVolume", CurrentVolume);
        AkSoundEngine.SetRTPCValue("MetronomeVolume", CurrentVolume);
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
            case TAudioBusType.kMetronome:
                {
                    AkSoundEngine.SetRTPCValue("MetronomeVolume", volume);
                }
                break;
            default:
            case TAudioBusType.kAll:
            {
                AkSoundEngine.SetRTPCValue("MasterVolume", volume);
                AkSoundEngine.SetRTPCValue("MusicVolume", volume);
                AkSoundEngine.SetRTPCValue("SFXVolume", volume);
                AkSoundEngine.SetRTPCValue("VOVolume", volume);
                AkSoundEngine.SetRTPCValue("MetronomeVolume", volume);
            }
                break;
        }
    }
}