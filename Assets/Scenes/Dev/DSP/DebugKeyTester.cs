using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugKeyTester : MonoBehaviour
{
    [SerializeField] private InputAction _action;
    [SerializeField] private NeedleAnimation _needleAnimation;

    private void Awake()
    {
        _action.started += OnButtonPressed;
        _action.canceled += OnButtonReleased;
        _action.Enable();
    }

    private void OnButtonPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Press!");
        _needleAnimation.GotToTheLowerLevel = true;
    }

    private void OnButtonReleased(InputAction.CallbackContext obj)
    {
        Debug.Log("NoPress!");
        _needleAnimation.GotToTheLowerLevel = false;
    }
}
