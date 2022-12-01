using Architecture;
using UnityEngine;


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
        _playerInputs.Menu.Enable();
    }
    void UpdateInputsOnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.OnMenuScene:
                EnableUI();
                break;
            case GameState.Pause:
                EnableUI();
                break;
            case GameState.SpawnCharacterStarts:
                EnablePlayer();
                break;
            case GameState.UnPause:
                EnablePlayer();
                break;
            case GameState.LevelStart:
                Debug.Log("Enable");
                _playerInputs.Player.Enable();
                break;
            case GameState.LevelEnd:
                Debug.Log("Disable");
                _playerInputs.Player.Disable();
                break;
            case GameState.StartDialogue:
                DisableMovement();
                break;
            case GameState.FinishDialogue:
                EnableMovement();
                break;
            case GameState.OnStartKinematic:
                DisableMovement();
                break;
            case GameState.OnEndKinematic:
                EnableMovement();
                break;
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
        Debug.Log("Stop Move");
        _playerInputs.Player.Move.Disable();

    }
    void EnableMovement()
    {
        _playerInputs.Player.Move.Enable();
    }

}
