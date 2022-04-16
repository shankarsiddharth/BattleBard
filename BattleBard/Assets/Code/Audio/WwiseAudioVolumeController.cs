using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
