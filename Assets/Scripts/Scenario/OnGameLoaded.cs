using UnityEngine;
using UnityEngine.Events;

public class OnGameLoaded : MonoBehaviour
{
    public UnityEvent OnGameLoadedEvent;

    private void Start()
    {
        OnGameLoadedEvent?.Invoke();
    }
}
