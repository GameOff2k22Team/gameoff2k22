using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : PlayerMovement
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnMovementPerformed()
    {
        Vector2 inputVector = _playerInputs.Player.Look.ReadValue<Vector2>();
        Utils.RecalculateVectorWithAngle(ref inputVector, Camera.main.ReturnCameraAngleCalculationInDegrees());
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        _controller.Move(movementVector * Time.deltaTime
            * PlayerSpeed);

        if (movementVector != Vector3.zero)
        {
            gameObject.transform.forward = movementVector;
        }
    }
}
