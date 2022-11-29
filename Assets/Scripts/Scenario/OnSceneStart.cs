using UnityEngine;
using UnityEngine.Events;

public class OnSceneStart : MonoBehaviour
{
    public UnityEvent OnGameLoadedEvent;

    private void Start()
    {
        OnGameLoadedEvent?.Invoke();
    }
}
