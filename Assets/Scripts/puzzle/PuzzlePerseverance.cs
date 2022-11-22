using Architecture;
using UnityEngine;

public class PuzzlePerseverance : MonoBehaviour
{
    public BoolVariable hasKey;
    public Animator animator;

    private PuzzleManager _puzzleManager;
    private bool _isOpen = false;
    private PuzzleManager.PuzzleType type = PuzzleManager.PuzzleType.PERSEVERANCE;

    // Start is called before the first frame update
    void Start()
    {
        this._puzzleManager = PuzzleManager.Instance;
    }

    public void OpenChest()
    {
        if (hasKey.Value &&
            this._isOpen == false && 
            !this._puzzleManager.perseverancePuzzleIsEnd)
        {
            this._isOpen = true;
            this._puzzleManager.OpenChest(type);
            this.animator.enabled = true;
        }
    }
}
