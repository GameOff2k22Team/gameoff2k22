using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using static UnityEditor.ArrayUtility;

public class MenuAction : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseWindow;
    private bool toggle = false;
    private Camera _cam;
    Action<InputAction.CallbackContext> lambdaHandler;
    private void Start()
    {
        lambdaHandler = context => Toggle();
        InputManager.Instance._playerInputs.Menu.OpenClose.performed += lambdaHandler;
        _cam = Camera.main;
    }

    private void OnDestroy()
    {
        InputManager.Instance._playerInputs.Menu.OpenClose.performed -= lambdaHandler;
    }

    public void Toggle()
    {
        toggle = !toggle;
        _pauseWindow.SetActive(toggle);
        if (toggle)
        {
            InputManager.Instance._playerInputs.UI.Enable();
            InputManager.Instance._playerInputs.Player.Disable();
            _cam.gameObject.SetActive(false);
        }
        else
        {
            InputManager.Instance._playerInputs.UI.Disable();
            InputManager.Instance._playerInputs.Player.Enable();
            _cam.gameObject.SetActive(true);
        }
    }
}
