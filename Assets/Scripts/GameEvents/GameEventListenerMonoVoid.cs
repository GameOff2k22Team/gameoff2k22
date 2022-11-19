using Architecture;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerMonoVoid : MonoBehaviour, IGameEventListener
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response = new UnityEvent();

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void RegisterListener(UnityAction action)
    {
        Response.AddListener(action);
        Event.RegisterListener(this);
    }

    public void UnregisterAllListeners()
    {
        Response.RemoveAllListeners();
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }

    public void OnEventRaised<T0>(T0 input)
    {
    }
}
