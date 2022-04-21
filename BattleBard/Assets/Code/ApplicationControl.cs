using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationControl : MonoBehaviour
{
    public void IncreaseVolume()
    {
       WwiseAudioVolumeController.IncreaseVolume();
    }


    public void DecreaseVolume()
    {
        WwiseAudioVolumeController.DecreaseVolume();
    }

    public void RestartLevel()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneNames.GameScene, LoadSceneMode.Single);
    }
    public void GoToMainMenu()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneNames.MainMenuScene, LoadSceneMode.Single);
    }

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
}
