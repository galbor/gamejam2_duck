using System.Collections.Generic;
using _SecondGameJam.Scripts.Data.EventListeners;
using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** A scriptable object used for creating new game events. */
    public abstract class GameEvent<T> : ScriptableObject
    {
        // List allows to organize the listeners' execution order. 
        private readonly List<GameEventListener<T>> _listeners = new();

        public void AddListener(GameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(GameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void Invoke(T item)
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventInvoked(item);
            }
        }
    }
}