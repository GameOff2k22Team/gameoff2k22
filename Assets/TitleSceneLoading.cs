using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneLoading : MonoBehaviour
{

    [SerializeField]
    private LoadingBar loadingBar;
    [SerializeField]
    private SceneName scene;

    private IEnumerator  Start()
    {
        AsyncOperation asyncOperation =
                SceneManager.LoadSceneAsync(scene.ToString());

        asyncOperation.allowSceneActivation = false;
        do
        {
            loadingBar.imageComp.fillAmount = asyncOperation.progress / 0.8f;
            yield return null;
        } while (asyncOperation.progress < 0.8);

        asyncOperation.allowSceneActivation = true;

    }
}
