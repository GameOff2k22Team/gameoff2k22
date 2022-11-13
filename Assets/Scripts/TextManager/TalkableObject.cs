using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableObject : MonoBehaviour
{
    public string DialogName;

    void Start()
    {
        EventsManager.TriggerEvent(EventsName.StartDialog.ToString(), new Args { message = DialogName });
    }
}
