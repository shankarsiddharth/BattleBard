using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverGameObject;
    public GameObject MetronomeCanvasGameObject;

    void Awake()
    {
        GameEvents.Instance.onBattalionKilled.AddListener(OnGameOver);
        gameOverGameObject.SetActive(false);
    }

    private void OnGameOver(Battalion arg0)
    {
        MetronomeCanvasGameObject.SetActive(false);
        WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kSFX);
        WwiseAudioVolumeController.SetWwiseAudioVolumeForAudioBusType(0, TAudioBusType.kMetronome);
        gameOverGameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneNames.GameScene, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
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
