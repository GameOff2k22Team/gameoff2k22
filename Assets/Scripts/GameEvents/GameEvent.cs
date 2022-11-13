using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Architecture
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IGameEventListener> eventListeners =
            new List<IGameEventListener>();

        public void Raise()
        {
            Debug.Log("Raised");
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                Debug.Log("InLoopRaise");
                eventListeners[i].OnEventRaised();

            }

        }

        public void Raise<T>(T input)
        {
            Debug.Log("GenericRaised");

            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(input);
        }

        public void RegisterListener(IGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}

