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


    public void PauseGame()
    {
        Debug.Log("game should be Paused");
        UpdateGameState(GameState.Pause);
    }

    public void UnPauseGame()
    {
        Debug.Log("game should be Paused");
        UpdateGameState(GameState.UnPause);
    }

    public void UpdateGameState(GameState newState)
    {
        _previousState = State;
        State = newState;
        switch (newState)
        {
            case GameState.SpawnCharacterStarts:
                break;
            case GameState.LevelStart:
                break;
            case GameState.LevelComplete:
                break;
            case GameState.LevelEnd:
                break;
            case GameState.Pause:
                OnGamePaused.Raise();
                break;
            case GameState.UnPause:
                OnGameUnpaused.Raise();
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
                if(_previousState == GameState.OnMenuScene)
                {
                    OnGameLoaded.Raise();
                }
                break;
            case GameState.OnMenuScene:
                OnMenuLoaded?.Raise();
                break;
            default:
                throw new ArgumentOutOfRangeException(
                                nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
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
    OnMenuScene
}