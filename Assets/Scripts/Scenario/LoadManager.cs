using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Help from : https://www.youtube.com/watch?v=OmobsXZSRKo
/// and https://www.youtube.com/watch?v=Oadq-IrOazg
/// </summary>
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    [SerializeField]
    private Animator _fadeAnimator;

    [Space]

    public UnityEvent OnFadeInCompleted;
    public UnityEvent OnFadeOutCompleted;

    private bool _fadeOutCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            OnFadeOutCompleted.AddListener(() => _fadeOutCompleted = true);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnFadeOutCompleted?.RemoveAllListeners();
        OnFadeInCompleted?.RemoveAllListeners();
    }

    public virtual void LoadScene(string scene)
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
