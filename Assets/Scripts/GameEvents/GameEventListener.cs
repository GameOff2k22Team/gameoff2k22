using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Architecture
{
    [Serializable]
    public class GameEventListener : IGameEventListener
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        private UnityEvent Response = new UnityEvent();

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

        public void UnregisterListener(UnityAction action)
        {
            Response.RemoveListener(action);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }

        public void OnEventRaised<T>(T input)
        {
        }
    }

    [Serializable]
    public class GameEventListener<T>: IGameEventListener
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        private UnityEvent<T> Response = new UnityEvent<T>();

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void RegisterListener(UnityAction<T> action)
        {
            Response.AddListener(action);
            Event.RegisterListener(this);
        }

        public void UnregisterAllListeners()
        {
            Response.RemoveAllListeners();
            Event.UnregisterListener(this);
        }

        public void UnregisterListener(UnityAction<T> action)
        {
            Response.RemoveListener(action);
        }

        public void OnEventRaised()
        {
        }

        public void OnEventRaised<T0>(T0 input)
        {
            switch(input)
            {
                case T listened:
                    Response.Invoke(listened);
                    break;

            }
             
        }
    }
}
