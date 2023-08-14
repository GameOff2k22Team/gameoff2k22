using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VikingCrew.Tools.UI;
using SpeechBubbleManager = VikingCrew.Tools.UI.SpeechBubbleManager;


public class DialogManager : Singleton<DialogManager>
{
    [Serializable]
    public struct MessageByUnit{
        public UnitManager.UnitType type;
        [TextAreaAttribute(1,10)]
        public string message;
    }

    private Action<InputAction.CallbackContext> lambdaHandler;
    [SerializeField]
    private AK.Wwise.Event SFXDialogPop = null;
    public void StartDialog(DialogTrigger dialogTrigger)
    {
        GameManager.Instance.UpdateGameState(GameState.StartDialogue);
        lambdaHandler = context => dialogTrigger.NextMessage();
        InputManager.Instance._playerInputs.Player.Use.performed += lambdaHandler;
        
        dialogTrigger.NextMessage();
    }

    public void FinishDialogue()
    {
        SpeechBubbleManager.Instance.Clear();
        InputManager.Instance._playerInputs.Player.Use.performed -= lambdaHandler;
        GameManager.Instance.UpdateGameState(GameState.FinishDialogue);
    }

    public SpeechBubbleBase SaySomething(MessageByUnit dialogInfo, Vector3 pannelScale, SpeechBubbleManager.SpeechbubbleType speechbubbleType)
    {
            SpeechBubbleManager.Instance.Clear();
            SpeechBubbleBase bubble = SpeechBubbleManager.Instance.AddSpeechBubble(UnitManager.Instance.GetUnitByType(dialogInfo.type).transform.position, 
                                                         pannelScale, dialogInfo.message, speechbubbleType);
        SFXDialogPop.Post(gameObject);
        return bubble;
    }
}
