using Architecture;
using System;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public BoolVariable hasKey;

    public PlayableLaunch openChestWithArtefact;

    public GameObject artefact;

    protected PuzzleManager _puzzleManager;
    public bool _isOpen = false;
    protected PuzzleManager.PuzzleType type;

    protected virtual void Start()
    {
        this._puzzleManager = PuzzleManager.Instance;
    }

    public virtual void OpenChest()
    {
        if (CheckIfCanBeOpened())
        {
            this._isOpen = true;
            this._puzzleManager.OpenChest(type, this);
        }
    }

    protected virtual bool CheckIfCanBeOpened()
    {
        return hasKey.Value &&
            this._isOpen == false;
    }
}