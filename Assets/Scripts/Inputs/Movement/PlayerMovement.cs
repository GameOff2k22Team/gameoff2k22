using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    protected InputsManager inputManager;
    protected PlayerInputActions _playerInputs;
    protected CharacterController _controller;
    protected Vector3 _playerVelocity;
    [SerializeField]
    protected bool _isGroundedPlayer;
    [SerializeField, Range(0, 50f)]
    protected float _playerSpeed = 2.0f;
    protected float _jumpHeight = 1.0f;
    protected float _gravityValue = -9.81f;
    private Camera _mainCamera;
    private Transform _mainCameraTr = null;
    private float _cameraAngle = 0f;


    private void Awake()
    {
        _playerInputs = InputsManager.Instance._playerInputs;

        if (!TryGetComponent<CharacterController>(out _controller))
        {
            _controller = gameObject.AddComponent<CharacterController>();
        }
        _mainCamera = Camera.main; 
        _mainCameraTr = _mainCamera.transform;
        _cameraAngle = CameraAngleCalculation();
    }

    private void OnEnable()
    {
        _playerInputs.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    protected virtual void Update()
    {
        CheckGrounded();
        OnMovementPerformed();
        ApplyGravity();
    }

    protected virtual void OnMovementPerformed()
    {
        Vector2 inputVector = _playerInputs.Player.Move.ReadValue<Vector2>();
        Utils.RecalculateVectorWithAngle(ref inputVector, _mainCamera.ReturnCameraAngleCalculationInDegrees());
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * _playerSpeed;
        
        _controller.Move(movementVector);

        if (movementVector != Vector3.zero)
        {
            gameObject.transform.forward = movementVector;
        }
    }

    protected void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (_isGroundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }

    protected virtual void CheckGrounded()
    {
        _controller.Move(_playerVelocity * Time.deltaTime);
        _isGroundedPlayer = _controller.isGrounded;
        if(_isGroundedPlayer && _playerVelocity.y < 0f)
        {
            _playerVelocity.y = 0f;
        }
    }

    protected virtual void ApplyGravity()
    {
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    

    protected float CameraAngleCalculation()
    {
        Vector3 cameraXProjected = Vector3.ProjectOnPlane(_mainCameraTr.right, Vector3.up);

        return Vector3.SignedAngle(cameraXProjected, Vector3.right, Vector3.up) *  Mathf.Deg2Rad;
    }

}

