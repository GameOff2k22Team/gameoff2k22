using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public SceneName nextScene;

    public virtual void GoToNextScene()
    {
        GameManager.Instance.UpdateGameState(
                            GameState.LoadNextScene);
        // TODO Add a blur
        SceneManager.LoadScene(nextScene.ToString(), 
                               LoadSceneMode.Single);
    }
}

public enum SceneName
{
    Title_Scene
}
