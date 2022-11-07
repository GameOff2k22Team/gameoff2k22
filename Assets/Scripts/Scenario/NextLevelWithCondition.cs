using Architecture;

public class NextLevelWithCondition : NextLevel
{
    public BoolVariable condition;

    public override void GoToNextScene()
    {
        if (condition)
        {
            base.GoToNextScene();
        }
    }
}
