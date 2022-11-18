using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public SceneName nextScene;

    bool isLoading;

    public virtual void GoToNextScene()
    {
        if (!isLoading)
        {
            LoadManager.Instance.LoadSceneInGame(nextScene.ToString());
            isLoading = true;
        }
    }
}

public enum SceneName
{
    Title_scene,
    Menu_scene,
    BedRoom
}
