using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement3D;

    [SerializeField]
    private PlayerMovement2D playerMovement2D;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameManager.OnGameStateChanged += UpdateInputsOnGameManagerStateChanged;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateInputsOnGameManagerStateChanged;

    }
    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameState.SpawnCharacterStarts);
    }


    private void UpdateInputsOnGameManagerStateChanged(GameState state)
    {
        if (state == GameState.BossCombat)
        {
            playerMovement3D.enabled = false;
            playerMovement2D.enabled = true;
        }
    }

    public void Disable3DMovement()
    {
        playerMovement3D.enabled = false;
        playerMovement2D.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
