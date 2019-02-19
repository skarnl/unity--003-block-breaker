using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void StartGame() {
        FindObjectOfType<GameState>().Reset();

        LoadNextScene();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }
}
