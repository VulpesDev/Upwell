﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private void Awake() {
        Instance = this;
    }

    public enum Scene {
        MainMenu,
        MovementTest,
        Level,
        GameOver,
        GameFinished,
    }

    void CheckForInput() {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
        if (Input.GetKeyDown(KeyCode.M)) // test phrase only
            LoadScene(Scene.MainMenu);
        // if(Input.GetKeyDown(KeyCode.Period))
        // {
        //     NextScene();
        // }
        // if (Input.GetKeyDown(KeyCode.Comma))
        // {
        //     PreviousScene();
        // }
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
