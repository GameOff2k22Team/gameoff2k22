using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePerseverance : MonoBehaviour
{

    private PuzzleManager _puzzleManager;
    [SerializeField]
    private Chest chest;
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
        if (chest.isOpen == false)
            this._puzzleManager.OpenChest(type, this.chest);
    }
}
