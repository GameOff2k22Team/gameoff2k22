using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public PlayerInputActions input {
        get => InputManager.Instance._playerInputs;
    }

    public UnityEvent OnInteraction;
    protected Action<InputAction.CallbackContext> LambdaHandler { 
        get => context => OnInteraction?.Invoke(); 
    }

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (CheckPlayer(other))
        {
            input.Player.Use.performed += LambdaHandler;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckPlayer(other))
        {
            input.Player.Use.performed -= LambdaHandler;
        }
    }

    protected bool CheckPlayer(Collider other)
    {
        return other.CompareTag(PLAYER_TAG);
    }
}
