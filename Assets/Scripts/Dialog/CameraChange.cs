using UnityEngine;
using VikingCrew.Tools.UI;

public class CameraChange : MonoBehaviour
{
    public Camera newCamera;
    private void OnEnable()
    {
        if (SpeechBubbleManager.Instance.gameObject.TryGetComponent(out Canvas canvas))
        {
            canvas.worldCamera = newCamera;
        }

        SpeechBubbleManager.Instance.Cam = newCamera;
    }
}
