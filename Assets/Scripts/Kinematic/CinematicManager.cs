using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : Singleton<CinematicManager>
{
    private GameManager _gameManager;
    private PlayableDirector _currentCinematic;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void StartCinemactic(PlayableDirector playableDirector)
    {
        if (_gameManager.State != GameState.OnStartKinematic)
        {
            _gameManager.UpdateGameState(GameState.OnStartKinematic);
            _currentCinematic = playableDirector;
            playableDirector.Play();
        }
    }

    private void Update()
    {
        if(_currentCinematic != null && _currentCinematic.state == PlayState.Paused)
        {
            _gameManager.UpdateGameState(GameState.OnEndKinematic);
        }
    }
}
