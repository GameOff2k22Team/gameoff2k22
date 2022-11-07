using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputsManager : Singleton<InputsManager>
{
    public PlayerInputActions _playerInputs;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _playerInputs = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
