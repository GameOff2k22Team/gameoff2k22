using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class WW_Material_Switch : MonoBehaviour
{
    public string StateGroup = "Footsteps_Forest_State";
    public string State = "Grass";
    public string ExitState = "Dirt";
    public GameObject Character;
    public bool Debug_Enabled;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }
        if (Debug_Enabled) { Debug.Log(State + " switch set"); }
        AkSoundEngine.SetState(StateGroup, State, Character);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") { return; }
        if (Debug_Enabled) { Debug.Log(ExitState + " switch set"); }
        AkSoundEngine.SetState(StateGroup, ExitState, Character);
    }
}
