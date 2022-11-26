using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameLoadedNotify : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameLoaded.Raise();
    }
}
