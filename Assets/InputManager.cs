using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Keybindings keybindings;

    void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public bool KeyDown(string key) {
        if (Input.GetKeyDown(keybindings.CheckKey(key)))
            return true;
        return false;
    }

    public void OnMove(InputAction.CallbackContext ctxt) {
        if (ctxt.performed)
            Debug.Log("move key detected");
    }

    public void OnJump(InputAction.CallbackContext ctxt) {
        if (ctxt.performed)
            Debug.Log("jump key detected");
    }
}
