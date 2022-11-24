using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    public struct Speed
    {
        [Range(0, 50f)]
        public float walkSpeed;
        [Range(0, 50f)]
        public float runSpeed;
    }

    
    public Speed speedInside;
    public Speed speedOutside;
    public bool isOutside;
    public Animator playerAnimator;

    protected InputManager inputManager;
    protected PlayerInputActions _playerInputs;
    protected CharacterController _controller;
    protected Vector3 _playerVelocity;
    [SerializeField]
    protected bool _isGroundedPlayer;
    protected float _jumpHeight = 1.0f;
    protected float _gravityValue = -9.81f;
    private Camera _mainCamera;
    private Transform _mainCameraTr = null;
    private float _cameraAngle = 0f;
    public AkEvent FootstepSound;
    private Speed _roomSpeed;
    private const string ANIMATOR_SPEED_VARIABLE = "Speed";

    private bool _isRunning;

    protected float PlayerSpeed
    {
        get
        {
            float playerSpeed = _isRunning ? _roomSpeed.runSpeed : _roomSpeed.walkSpeed;
            return playerSpeed;
        }
    }


    private void Awake()
    {
        _playerInputs = InputManager.Instance._playerInputs;

        if (!TryGetComponent<CharacterController>(out _controller))
        {
            _controller = gameObject.AddComponent<CharacterController>();
        }

        _roomSpeed = isOutside ? speedOutside : speedInside;

        _mainCamera = Camera.main; 
        _mainCameraTr = _mainCamera.transform;
        _cameraAngle = CameraAngleCalculation();
    }

    void Anim_Footstep()
    {
        if(FootstepSound != null)
        {
            FootstepSound.HandleEvent(gameObject);
        }
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
        
        if (inputVector.magnitude < 0.03)
        {
            Idle();
        }
        else if (inputVector.magnitude > 0.9 ||
                 _playerInputs.Player.Run.IsPressed())
        {
            Run();
        } else
        {
            Walk();
        }

        Utils.RecalculateVectorWithAngle(ref inputVector, _mainCamera.ReturnCameraAngleCalculationInDegrees());
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * PlayerSpeed;

        _controller.Move(movementVector);

        if (movementVector != Vector3.zero)
        {
            gameObject.transform.forward = movementVector;
        }
    }

    protected void Idle()
    {
        playerAnimator.SetFloat(ANIMATOR_SPEED_VARIABLE, 0);
        _isRunning = false;
    }

    protected void Walk()
    {
        playerAnimator.SetFloat(ANIMATOR_SPEED_VARIABLE, 0.5f);
        _isRunning = false;
    }

    protected void Run()
    {
        playerAnimator.SetFloat(ANIMATOR_SPEED_VARIABLE, 1);
        _isRunning = true;
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

