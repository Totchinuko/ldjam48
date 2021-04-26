using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DebugKeyTest : MonoBehaviour
{ 
    public InputAction action;
    private void Awake() { 
        action.started += obj => Debug.Log("Pressed");
        action.canceled += obj => Debug.Log("Released");
        action.Enable(); 
        }
    }
