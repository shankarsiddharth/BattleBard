using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public int deltaVolume = 5;
    public int minimumVolume = 0;
    public int maximumVolume = 100;
    public List<Resolution> resolutionOptions;// = Screen.resolutions.ToList();
    private int _currentResolutionIndex;
    private int _currentVolume;

    public Text resolutionText; 
    public Text volumeText;

    public Slider VolumeSlider;

    public void IncreaseResolution()
    {
        _currentResolutionIndex++;
        if (_currentResolutionIndex >= resolutionOptions.Count)
        {
            _currentResolutionIndex = resolutionOptions.Count - 1;
        }
        UpdateResolutionText();
    }

    public void DecreaseResolution()
    {
        _currentResolutionIndex--;
        if (_currentResolutionIndex < 0)
        {
            _currentResolutionIndex = 0;
        }
        UpdateResolutionText();
    }

    public void ChangeResolution()
    {
        _currentResolutionIndex++;
        if (_currentResolutionIndex >= resolutionOptions.Count)
        {
            _currentResolutionIndex = 0;
        }

        UpdateResolutionText();
    }

    public void UpdateResolutionText()
    {
        resolutionText.text = resolutionOptions[_currentResolutionIndex].width.ToString() + " x " +
                              resolutionOptions[_currentResolutionIndex].height.ToString();
    }

    public void ApplyChanges()
    {
        Screen.SetResolution(resolutionOptions[_currentResolutionIndex].width, resolutionOptions[_currentResolutionIndex].height, true);
        SetCurrentVolumeLevel();
    }


    public void IncreaseVolume()
    {
        _currentVolume += deltaVolume;
        if (_currentVolume >= maximumVolume)
        {
            _currentVolume = maximumVolume;
        }

        VolumeSlider.value = (float) (_currentVolume / 100.0f);
        UpdateVolumeText();
    }


    public void DecreaseVolume()
    {
        _currentVolume -= deltaVolume;
        if (_currentVolume < 0)
        {
            _currentVolume = minimumVolume;
        }

        VolumeSlider.value = (float)(_currentVolume / 100.0f);
        UpdateVolumeText();
    }
    

    public void OnVolumeSliderChanged(float value)
    {
        _currentVolume = ((int)Math.Round(value * 100.0f));
        UpdateVolumeText();
    }

    private void UpdateVolumeText()
    {
        volumeText.text = _currentVolume.ToString();
    }

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        resolutionOptions = Screen.resolutions.ToList();
        bool isResolutionPresentInOptions = false;
        for (int i = 0; i < resolutionOptions.Count; i++)
        {
            if (Screen.width == resolutionOptions[i].width
                && Screen.height == resolutionOptions[i].height)
            {
                isResolutionPresentInOptions = true;
                _currentResolutionIndex = i;
            }
        }

        if (!isResolutionPresentInOptions)
        {
            Resolution newResolutionData = new Resolution
            {
                width = Screen.width,
                height = Screen.height
            };
            resolutionOptions.Add(newResolutionData);
            _currentResolutionIndex = resolutionOptions.Count - 1;
        }

        UpdateResolutionText();
        GetCurrentVolumeLevel();
        SetCurrentVolumeLevel();
    }

    private int GetCurrentVolumeLevel()
    {
        _currentVolume = ((int)Math.Round(VolumeSlider.value * 100.0f));
        return _currentVolume;
    }

    private void SetCurrentVolumeLevel()
    {
        UpdateVolumeText();
        //TODO: Add code to get the volume from Wwise
        WwiseAudioVolumeController.SetWwiseAudioVolume(_currentVolume);
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
