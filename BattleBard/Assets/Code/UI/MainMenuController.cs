using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: Handle Input Actions based on Hardware Button Inputs

public class MainMenuController : MonoBehaviour
{
    public string GameSceneName = "GameLevel";
    public GameObject MainMenuScreen;
    public GameObject OptionsScreen;
    public GameObject CreditsScreen;

    private GameObject wwiseGameObject;
    private BGAudio bgAudio;

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnCreditsButtonClicked()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(true);

    }

    public void OnOptionsButtonClicked()
    {
        MainMenuScreen.SetActive(false);
        OptionsScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        bgAudio.StopBGAudio();
        SceneManager.LoadSceneAsync(GameSceneName);
    }

    public void OnBackToMainMenuClicked()
    {
        MainMenuScreen.SetActive(true);
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    void Awake()
    {
        wwiseGameObject = GameObject.FindGameObjectWithTag("Wwise");
        if (wwiseGameObject == null)
        {
            throw new NullReferenceException("wwiseGameObject is null in MainMenuController");
        }
        bgAudio = wwiseGameObject.GetComponentInChildren<BGAudio>();
        if (bgAudio == null)
        {
            throw new NullReferenceException("bgAudio is null in MainMenuController");
        }
        if (MainMenuScreen == null || OptionsScreen == null
                                   || CreditsScreen == null)
        {
            throw new NullReferenceException("MainMenuScreen or OptionsScreen or CreditsScreen is null in MainMenuController");
        }

        MainMenuScreen.SetActive(true);
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(false);

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
}
