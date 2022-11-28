using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHidden : PuzzleBase
{
    public bool _realChest = false;
    protected override void Start()
    {
        base.Start();
        type = PuzzleManager.PuzzleType.FOCUS;
    }

    public override void OpenChest()
    {
        base.OpenChest();
        this._isOpen = false;

    }
}
