using UnityEngine;
using VikingCrew.Tools.UI;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public Vector3 pannelScale = new Vector3(0.01f, 0.01f, 0.01f);
    private int currentMsgIdx = -1;

    private SpeechBubbleTmp currentBubble;

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
        if (currentMsgIdx >= dialog.listOfMessageByUnitType.Count - 1 &&
            currentBubble.CheckTextDisplayed())
        {
            DialogManager.Instance.FinishDialogue();
            currentMsgIdx = -1;
        }
        else
        {
            bool finishTheBubbleText = currentBubble != null && !currentBubble.CheckTextDisplayed();
            if (finishTheBubbleText)
            {
                currentBubble.DisplayAllText();
            } else
            {
                currentMsgIdx++;
                SpeechBubbleManager.SpeechbubbleType type = SpeechBubbleManager.SpeechbubbleType.NORMAL;
                DialogManager.MessageByUnit msg = 
                        dialog.listOfMessageByUnitType[currentMsgIdx];

                currentBubble = DialogManager.Instance.SaySomething(msg, 
                                                                    pannelScale,
                                                                    type) as SpeechBubbleTmp;
            }
        }
    }
}
