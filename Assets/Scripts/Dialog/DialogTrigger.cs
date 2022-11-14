using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    private void OnTriggerEnter(Collider other)
    {
        if (dialog.canBeTrigger)
        {
            DialogManager.Instance.StartDialog(dialog.listOfMessageByUnitType);
            if (dialog.onlyOneTrigger)
            {
                dialog.canBeTrigger = false;
            }
        }
        
        
    }
}
