using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Help from : https://www.youtube.com/watch?v=OmobsXZSRKo
/// and https://www.youtube.com/watch?v=Oadq-IrOazg
/// </summary>
public class LoadManager : Singleton<LoadManager>
{
    [SerializeField]
    private Animator _fadeAnimator;

    [Space]

    public UnityEvent OnFadeInCompleted;
    public UnityEvent OnFadeOutCompleted;

    private bool _fadeOutCompleted;

    public override void Awake()
    {
        base.Awake();
        OnFadeOutCompleted.AddListener(() => _fadeOutCompleted = true);
    }

    private void OnDestroy()
    {
        OnFadeOutCompleted?.RemoveAllListeners();
        OnFadeInCompleted?.RemoveAllListeners();
    }

    public virtual void LoadSceneInGame(string scene)
    {
        GameManager.Instance.UpdateGameState(
                            GameState.LoadNextScene);

        _fadeAnimator.SetTrigger("FadeOut");

        StartCoroutine(WaitToLoadLevel(scene));

        // If we want to use a LoadScene --> Load it here
    }

    public void FadeOutCompleted()
    {
        OnFadeOutCompleted?.Invoke();
    }

    public void FadeInCompleted()
    {
        OnFadeInCompleted?.Invoke();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator WaitToLoadLevel(string scene)
    {
        AsyncOperation asyncOperation =
                SceneManager.LoadSceneAsync(scene);

        asyncOperation.allowSceneActivation = false;

        do {
            yield return null;
        } while (!_fadeOutCompleted || asyncOperation.progress < 0.8);

        asyncOperation.allowSceneActivation = true;

        _fadeAnimator.SetTrigger("FadeIn");

        _fadeOutCompleted = false;
    }
}
