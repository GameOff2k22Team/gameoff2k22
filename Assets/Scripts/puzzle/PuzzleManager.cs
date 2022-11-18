using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{

    public enum PuzzleType { PERSEVERANCE, SPEED, FOCUS }

    public struct PerseverancePuzzle
    {
        public PuzzleType type;
        public int numberOfTryRequired;
        public int numberOfTry;

        public PerseverancePuzzle(PuzzleType pType, int nbTryRequried)
        {
            this.type = pType;
            this.numberOfTryRequired = nbTryRequried;
            this.numberOfTry = 1;

        }

    }

    public int perseverancePuzzleNumberOfTryRequired = 15;

    public PerseverancePuzzle perseverancePuzzle;

    // Start is called before the first frame update
    void Start()
    {
        perseverancePuzzle = new PerseverancePuzzle(PuzzleType.PERSEVERANCE, perseverancePuzzleNumberOfTryRequired);
        
    }

    public void OpenChest(PuzzleType puzzleType, Chest chest)
    {
        switch (puzzleType)
        {
            case PuzzleType.PERSEVERANCE:
                this.OpenChestPerseverance(chest);
                break;
            default: break;
        }
    }

    private void OpenChestPerseverance(Chest chest)
    {
        chest.isOpen = true;
        if (this.perseverancePuzzle.numberOfTry == this.perseverancePuzzle.numberOfTryRequired)
        {
            this.GiveArtefact();
        }
        else
        {
            this.AddTry();
        }
    }

    private void AddTry()
    {
        this.perseverancePuzzle.numberOfTry += 1;
    }
    
    private void GiveArtefact()
    {
        Debug.Log("GiveArtefact");
    }
}
