using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public PlayerInputActions player;

    public UnityEvent OnInteraction;
    protected Action<InputAction.CallbackContext> LambdaHandler { 
        get => context => OnInteraction?.Invoke(); 
    }

    private const string PLAYER_TAG = "Player";

    protected virtual void Awake()
    {
        player = new PlayerInputActions();
        player.Player.Enable();
    }

    private void OnDestroy()
    {
        player.Player.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckPlayer(other))
        {
            player.Player.Use.performed += LambdaHandler;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckPlayer(other))
        {
            player.Player.Use.performed -= LambdaHandler;
        }
    }

    protected bool CheckPlayer(Collider other)
    {
        return other.CompareTag(PLAYER_TAG);
    }
}
