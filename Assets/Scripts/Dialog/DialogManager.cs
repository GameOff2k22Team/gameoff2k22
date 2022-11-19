using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;


public class DialogManager : Singleton<DialogManager>
{
    [Serializable]
    public struct MessageByUnit{
        public UnitManager.UnitType type;
        public string message;
    }

    public void StartDialog(List<MessageByUnit> dialog)
    {
        GameManager.Instance.UpdateGameState(GameState.StartDialogue);
        foreach (MessageByUnit dialogInfo in dialog)
        {
            SaySomething(dialogInfo, SpeechBubbleManager.SpeechbubbleType.NORMAL);
        }
        GameManager.Instance.UpdateGameState(GameState.FinishDialogue);

    }
    public void SaySomething(MessageByUnit dialogInfo, SpeechBubbleManager.SpeechbubbleType speechbubbleType)
    {
            SpeechBubbleManager.Instance.AddSpeechBubble(UnitManager.Instance.GetUnitByType(dialogInfo.type).transform.position , dialogInfo.message, speechbubbleType);
    }
}
