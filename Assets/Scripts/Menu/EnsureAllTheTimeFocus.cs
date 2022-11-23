using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class EnsureAllTheTimeFocus : MonoBehaviour
{
    [Header("Home menu")]
    [SerializeField]
    private List<GameObject> _buttons;
    [SerializeField]
    private GameObject _firstButton;

    [Header("Settings menu")]
    [SerializeField]
    private GameObject _settingsPanel;
    [SerializeField]
    private GameObject _firstSettingsButton;
    [SerializeField]
    private List<GameObject> _settingsButtons;
    private GameObject _lastSelectedGameObject;

    Action<InputAction.CallbackContext> lambdaHandler;

    private void Start()
    {
        _lastSelectedGameObject = _firstButton;
        Debug.Log("Test");
        lambdaHandler = context => SetFocus();
        InputManager.Instance._playerInputs.UI.Navigate.performed += lambdaHandler;
        EventSystem.current.SetSelectedGameObject(_firstButton);
    }

    private void OnDisable()
    {
        InputManager.Instance._playerInputs.UI.Navigate.performed -= lambdaHandler;
    }

    private void SetFocus()
    {
         if (!_buttons.Contains(EventSystem.current.currentSelectedGameObject))
        {
            EventSystem.current.SetSelectedGameObject(_lastSelectedGameObject);
        }
        if (_lastSelectedGameObject != EventSystem.current.currentSelectedGameObject)
        {
            _lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        //if (_settingsPanel.activeSelf && !_settingsButtons.Contains(EventSystem.current.currentSelectedGameObject))
        //{
        //    EventSystem.current.SetSelectedGameObject(_firstSettingsButton);
        //}
    }

}
