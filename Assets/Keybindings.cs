using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    public KeyCode jump, jet, left, right;

    public KeyCode CheckKey(string key) {
        switch (key)
        {
            case "jump":
                return jump;
            case "jet":
                return jet;
            case "left":
                return left;
            case "right":
                return right;
            default:
                return KeyCode.None;
        }
    }
}
