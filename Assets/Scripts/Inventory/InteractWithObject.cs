using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithObject : MonoBehaviour
{
    public PlayerInputActions player;

    public PlayerInputActions.IPlayerActions input;

    private const string INTERACTABLE_TAG = "Interactable";

    private void Awake()
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
        if (CheckInteractable(other))
        {
            var interaction = other.gameObject.GetComponentInParent<Interaction>();
            player.Player.Use.performed += 
                (InputAction.CallbackContext context) => interaction.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckInteractable(other))
        {
            var interaction = other.gameObject.GetComponentInParent<Interaction>();
            player.Player.Use.performed -=
                (InputAction.CallbackContext context) => interaction.Interact();
        }
    }

    private bool CheckInteractable(Collider other)
    {
        return other.CompareTag(INTERACTABLE_TAG);
    }
}
