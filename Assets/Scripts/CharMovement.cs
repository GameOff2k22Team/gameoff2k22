using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    protected CharacterController controller;
    protected Vector3 playerVelocity;
    protected bool groundedPlayer;
    [SerializeField,Range(0,10f)]
    protected float playerSpeed = 2.0f;
    protected float jumpHeight = 1.0f;
    protected float gravityValue = -9.81f;
    protected string horizAxisName = "Horizontal";
    protected string vertAxisName = "Vertical";


    protected virtual void Start()
    {
        if (!TryGetComponent<CharacterController>(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    protected virtual void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis(horizAxisName), 0, Input.GetAxis(vertAxisName));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
