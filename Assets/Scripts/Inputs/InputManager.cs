using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : Singleton<InputManager>
{
    public PlayerInputActions _playerInputs;

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
        OnMenuLoaded.RegisterListener(EnableUI);
        OnGameLoaded.RegisterListener(EnablePlayer);
        OnGamePaused.RegisterListener(EnableUI);
        OnGameUnpaused.RegisterListener(EnablePlayer);

    }

    public void OnDestroy()
    {
        OnMenuLoaded.UnregisterListeners();
        OnGamePaused.UnregisterListeners();
        OnGameUnpaused.UnregisterListeners();
    }

    // Update is called once per frame
    void EnableUI()
    {
        Debug.Log("Enabled UI");
        _playerInputs.Player.Disable();
        _playerInputs.UI.Enable();
    }

    void EnablePlayer()
    {
        _playerInputs.UI.Disable();
        _playerInputs.Player.Enable();
    }

}
