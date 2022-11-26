using Architecture;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{ 
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [Header ("Game Events")]
    public GameEvent OnMenuLoaded;
    public GameEvent OnGameLoaded;
    public GameEvent OnSceneLoaded;

    public GameEvent OnGamePaused;
    public GameEvent OnGameUnpaused;

    [Space]
    [Header("Listeners")]
    public GameEventListener<SceneName> OnSceneChanged;



    private GameState _previousState;


    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {        
        UpdateGameState(GameState.OnMenuScene);
    }

    public void UpdateGameState(GameState newState)
    {
        _previousState = State;
        Debug.Log(newState);
        State = newState;
        switch (newState)
        {
            case GameState.SpawnCharacterStarts:
                break;
            case GameState.LevelStart:
                OnSceneLoaded.Raise();
                break;
            case GameState.LevelComplete:
                break;
            case GameState.LevelEnd:
                break;
            case GameState.Pause:
                break;
            case GameState.UnPause:
                break;
            case GameState.PlayerDeath:
                break;
            case GameState.SpawnComplete:
                break;
            case GameState.Respawn:
                break;
            case GameState.GameOver:
                break;
            case GameState.LoadNextScene:
                break;
            case GameState.OnMenuScene:
                break;
            case GameState.StartDialogue:
                break;
            case GameState.FinishDialogue:
                break;
            default:
                throw new ArgumentOutOfRangeException(
                                nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLoadNextScene()
    {
    }
}
public enum GameState
{
    SpawnCharacterStarts,
    LevelStart,
    LevelComplete,
    LevelEnd,
    Pause,
    UnPause,
    PlayerDeath,
    SpawnComplete,
    Respawn,
    GameOver,
    LoadNextScene,
    OnMenuScene,
    StartDialogue,
    FinishDialogue,
    BossCombat
}