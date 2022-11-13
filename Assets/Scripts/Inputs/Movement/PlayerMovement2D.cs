using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    protected PlayerInputActions _playerInputs;
    [SerializeField, Range(0f, 100f)]
    protected float _playerSpeed = 2.0f;
    [SerializeField, Range(0f, 100f)]
    protected float _maxSpeed = 5.0f;
    protected Transform _playerTR;
    protected Rigidbody2D _player2Drb;

    void Awake()
    {
        _playerInputs = InputManager.Instance._playerInputs;
        _playerTR = this.transform;
        _player2Drb = this.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInputs.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = _playerInputs.Player.Move.ReadValue<Vector2>();
        var playerMovement = inputVector * _playerSpeed * Time.deltaTime;
        var playerMovement3D = new Vector3(playerMovement.x, playerMovement.y, 0);
        _player2Drb.MovePosition(_playerTR.position + playerMovement3D );
    }
}
