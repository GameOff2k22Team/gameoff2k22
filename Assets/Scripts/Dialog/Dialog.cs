using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public List<DialogManager.MessageByUnit> listOfMessageByUnitType;
    public bool onlyOneTrigger = false;
    public bool canBeTrigger = true;

    public void OnEnable()
    {
        this.canBeTrigger = true;
    }
}