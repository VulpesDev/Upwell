using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private void Awake() {
        // if (!Instance)
            Instance = this;
        // else if (Instance != this)
        //     Destroy(this);
        // DontDestroyOnLoad(this);
    }

    public enum Scene {
        MainMenu,
        Option,
        MarioMovement1,
        GameOver,
        GameFinished,
    }

    //test only
    void CheckForInput() {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
        if (Input.GetKeyDown(KeyCode.M)) // test phrase only
            LoadScene(Scene.MainMenu);
    }

    void Update() {
        CheckForInput();
    }

    public void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // public void NextScene()
    // {
    //     if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

    // public void PreviousScene()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex - 1 >= 0)
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    // }
}
