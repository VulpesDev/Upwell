using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            LoadSceneByIndex(0);
        }
    }
    public void LoadNextScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Calculate the next scene's build index
        int nextSceneIndex = currentScene.buildIndex + 1;

        // Check if the next scene index is within the range of added scenes
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Next scene index is out of range. No more scenes to load.");
        }
    }

    // Method to load a scene by its name
    public void LoadSceneByName(string sceneName)
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        // Check if the scene index is within the range of added scenes
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the scene by index
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Scene index is out of range.");
        }
    }
}
