using Architecture;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelWithCondition : NextLevel
{
    public BoolVariable condition;

    [Space]
    public UnityEvent OnRightCondition;
    public UnityEvent OnWrongCondition;
    public override void GoToNextScene()
    {
        if (condition.Value)
        {
            base.GoToNextScene();
            OnRightCondition?.Invoke();
        } else
        {
            OnWrongCondition?.Invoke();
        }
    }
}
