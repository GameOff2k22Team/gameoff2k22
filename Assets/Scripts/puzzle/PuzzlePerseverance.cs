public class PuzzlePerseverance : PuzzleBase
{
    protected override void Start()
    {
        base.Start();
        type = PuzzleManager.PuzzleType.PERSEVERANCE;
    }

    protected override bool CheckIfCanBeOpened()
    {
        return base.CheckIfCanBeOpened() &&
            !this._puzzleManager.perseverancePuzzleIsEnd;
    }
}
