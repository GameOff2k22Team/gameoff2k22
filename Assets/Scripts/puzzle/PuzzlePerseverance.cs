using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePerseverance : MonoBehaviour
{

    private PuzzleManager _puzzleManager;
    private bool _isOpen = false;
    private PuzzleManager.PuzzleType type = PuzzleManager.PuzzleType.PERSEVERANCE;

    // Start is called before the first frame update
    void Start()
    {
        this._puzzleManager = PuzzleManager.Instance;
    }

    // Update is called once per frame
    //void Update()
    //{

    //    if (chest.isOpen == false)

    //        this._puzzleManager.OpenChest(type, this.chest);

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (this._isOpen == false && !this._puzzleManager.perseverancePuzzleIsEnd)
        {
            this._isOpen = true;
            this._puzzleManager.OpenChest(type);
            this.gameObject.SetActive(false); // This must be replaced by the opening chest animation
        }
    }
}
