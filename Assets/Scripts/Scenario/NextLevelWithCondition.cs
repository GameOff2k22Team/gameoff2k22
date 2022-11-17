using Architecture;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelWithCondition : NextLevel
{
    public BoolVariable condition;

    [Space]
    public UnityEvent OnWrongCondition;
    public override void GoToNextScene()
    {
        if (condition.Value)
        {
            base.GoToNextScene();
        } else
        {
            OnWrongCondition?.Invoke();
        }
    }
}
