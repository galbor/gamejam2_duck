using _SecondGameJam.Scripts.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace _SecondGameJam.Scripts.Data.EventListeners
{
    /** A component that listens to game events.
     * Add this component to any GameObject that needs to listen to game events.*/
    public abstract class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField] protected GameEvent<T> _subscribedEvent;

        [SerializeField] protected UnityEvent<T> _listenerResponse;

        private void OnEnable() => _subscribedEvent.AddListener(this);

        private void OnDisable() => _subscribedEvent.RemoveListener(this);

        public void OnEventInvoked(T item)
        {
            _listenerResponse?.Invoke(item);
        }
    }
}