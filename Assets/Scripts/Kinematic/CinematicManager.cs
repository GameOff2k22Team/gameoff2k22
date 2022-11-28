using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : Singleton<CinematicManager>
{
    private GameManager _gameManager;
    private PlayableDirector _currentCinematic;
    private bool hasAlreadyStopped;

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
            hasAlreadyStopped = false;
        }
    }

    private void Update()
    {
        if(_currentCinematic != null && _currentCinematic.state == PlayState.Paused &&
           !hasAlreadyStopped)
        {
            _gameManager.UpdateGameState(GameState.OnEndKinematic);
            hasAlreadyStopped = true;
        }
    }
}
