using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public GameObject gameWonGameObject;
    public GameObject MetronomeCanvasGameObject;

    void Awake()
    {
        GameEvents.Instance.onGameWon.AddListener(OnGameWon);
        gameWonGameObject.SetActive(false);
    }

    private void OnGameWon()
    {
        MetronomeCanvasGameObject.SetActive(false);
        WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kSFX);
        WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kMetronome);
        gameWonGameObject.SetActive(true);
    }

    public void RestartGame()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneNames.GameScene, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneNames.MainMenuScene, LoadSceneMode.Single);
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
