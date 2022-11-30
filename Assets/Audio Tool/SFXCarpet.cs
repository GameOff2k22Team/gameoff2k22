using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCarpet : MonoBehaviour
{
    public AkEvent SFXCarpetForest;

    void Anim_SFX_Carpet()
    {
        SFXCarpetForest.HandleEvent(gameObject);
    }

}
