using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void UpdateGameState(GameState newState)
    {
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
    LoadNextScene
}