using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Chest : MonoBehaviour
{
    public AkEvent SFXFallChest;
    public AkEvent SFXOpenChest;
    
    void Anim_Fall_Chest()
    {
        SFXFallChest.HandleEvent(gameObject);
    }
    void Anim_Open_Chest()
    {
        SFXOpenChest.HandleEvent(gameObject);
    }

}
