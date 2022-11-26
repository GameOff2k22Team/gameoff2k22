using UnityEngine;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public Vector3 pannelScale = new Vector3(0.01f, 0.01f, 0.01f);
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
                                                pannelScale,
                                       SpeechBubbleManager.SpeechbubbleType.NORMAL);

        }
    }
}
