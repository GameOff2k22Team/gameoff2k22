using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public SceneName nextScene;

    public virtual void GoToNextScene()
    {
        LoadManager.Instance.LoadScene(nextScene.ToString());
    }
}

public enum SceneName
{
    Title_scene,
    Menu_scene,
    Room
}
