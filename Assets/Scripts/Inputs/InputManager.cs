using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : Singleton<InputManager>
{
    public PlayerInputActions _playerInputs;

    [SerializeField]
    private BoolVariable DebugMode;

    [SerializeField]
    private GameEventListener OnMenuLoaded;
    [SerializeField]
    private GameEventListener OnGameLoaded;
    [SerializeField]
    private GameEventListener OnGamePaused;
    [SerializeField]
    private GameEventListener OnGameUnpaused;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _playerInputs = new PlayerInputActions();
        
        GameManager.OnGameStateChanged += UpdateInputsOnGameManagerStateChanged;

    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateInputsOnGameManagerStateChanged;

    }

    private void Start()
    {
        if (DebugMode.Value)
        {
            _playerInputs.Player.Enable();
        }
    }
    void UpdateInputsOnGameManagerStateChanged(GameState state)
    {
        if (state == GameState.OnMenuScene ||
            state == GameState.Pause )
        {
            EnableUI();
        }
        else if (state == GameState.SpawnCharacterStarts ||
                 state == GameState.UnPause)
        {
            EnablePlayer();
        }

        else if (state == GameState.StartDialogue)
        {
            DisableMovement();
        }
        else if (state == GameState.FinishDialogue)
        {
            EnableMovement();
        }
    }


    void EnableUI()
    {
        _playerInputs.Player.Disable();
        _playerInputs.UI.Enable();
    }

    void EnablePlayer()
    {
        Debug.Log("Enabling Player");

        _playerInputs.UI.Disable();
        _playerInputs.Player.Enable();
    }

    void DisableMovement()
    {
        _playerInputs.Player.Move.Disable();

    }
    void EnableMovement()
    {
        _playerInputs.Player.Move.Enable();
    }

}
