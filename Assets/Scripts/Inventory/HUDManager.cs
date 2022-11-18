using UnityEngine;
using Architecture;

public class HUDManager : Singleton<HUDManager>
{
    public Canvas canvas;
    public GameEvent onSceneLoaded;

    private GameEventListener listener = new GameEventListener();
    private const string UI_CAMERA_TAG = "UICamera";

    public override void Awake()
    {
        base.Awake();

        listener.Event = onSceneLoaded;
        listener.RegisterListener(SetNewUICamera);
    }

    public void SetNewUICamera()
    {
        foreach (Camera camera in Camera.allCameras)
        {
            if (camera.CompareTag(UI_CAMERA_TAG))
            {
                canvas.worldCamera = camera;
                Debug.Log(canvas.worldCamera);
                break;
            }
        }
    }
}
