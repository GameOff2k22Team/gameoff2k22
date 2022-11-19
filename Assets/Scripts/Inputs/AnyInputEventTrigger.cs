using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AnyInputEventTrigger : MonoBehaviour
{
    public UnityEvent OnAnyKeyPressed;
    protected Action<InputAction.CallbackContext> LambdaHandler
    {
        get => context => OnAnyKeyPressed?.Invoke();
    }

    private void Start()
    {
        InputManager.Instance._playerInputs.UI.AnyKey.performed += LambdaHandler;
    }

    private void OnDestroy()
    {
        InputManager.Instance._playerInputs.UI.AnyKey.performed -= LambdaHandler;
    }
}
