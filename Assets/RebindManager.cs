using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindManager : MonoBehaviour
{
    public InputActionReference MoveRef, JumpRef;

    private void OnEnable() {
        MoveRef.action.Disable();
        JumpRef.action.Disable();
    }

    private void OnDisable() {
        MoveRef.action.Enable();
        JumpRef.action.Enable();
    }
}
