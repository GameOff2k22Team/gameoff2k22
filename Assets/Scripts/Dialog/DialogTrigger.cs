using UnityEngine;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    private int currentMsgIdx = -1;
    public void LaunchDialog()
    {
        if (dialog.canBeTrigger && currentMsgIdx == -1)
        {
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
            DialogManager.Instance.FinishDialogue();
            currentMsgIdx = -1;
        }
        else
        {
            DialogManager.MessageByUnit msg = 
                    dialog.listOfMessageByUnitType[currentMsgIdx];
            DialogManager.Instance.SaySomething(msg, 
                                       SpeechBubbleManager.SpeechbubbleType.NORMAL);

        }
    }
}
