using UnityEngine;

public class OnScreenControllerManager : MonoBehaviour
{
    public GameObject onScreenController;

#if UNITY_ANDROID
    public void Awake()
    {
        onScreenController.SetActive(true);
    }
#endif
}
