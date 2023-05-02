using UnityEngine;
using UnityEngine.Events;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public Vector3 pannelScale = new Vector3(0.01f, 0.01f, 0.01f);
    private int currentMsgIdx = -1;

    public UnityEvent onStartOfDialogue; 
    public UnityEvent onEndOfDialogue; 

    public void LaunchDialog()
    {
        if (dialog.canBeTrigger && currentMsgIdx == -1)
        {
            onStartOfDialogue?.Invoke();
            DialogManager.Instance.StartDialog(this);
            if (dialog.onlyOneTrigger)
            {
                dialog.canBeTrigger = false;
            }
        }
    }

    public void NextMessage()
    {
        currentMsgIdx++;
        if (currentMsgIdx >= dialog.listOfMessageByUnitType.Count)
        {
            onEndOfDialogue?.Invoke();
            DialogManager.Instance.FinishDialogue();
            currentMsgIdx = -1;
        }
        else
        {
            DialogManager.MessageByUnit msg = 
                    dialog.listOfMessageByUnitType[currentMsgIdx];
            DialogManager.Instance.SaySomething(msg,
                                                pannelScale,
                                       SpeechBubbleManager.SpeechbubbleType.NORMAL);

        }
    }
}
