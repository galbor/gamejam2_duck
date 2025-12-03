using System;
using System.Collections.Generic;
using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Gameplay.Enemies;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /**
     * Allows for loosely coupled communication between classes, making it easier to swap out implementations.
     * Mainly used for UI and game events(triggers such as player progress and notifications of world changes).
     * Cons:
     * 1. Hard to track which events are being published and subscribed to.
     * 2. No way to enforce a contract for event data.
     * 3. Potential for memory leaks if listeners are not unsubscribed.
     * 4. No way to prioritize event listeners.
     * 5. No way to prevent event listeners from being called in certain situations/multiple times.
     * 6. Potentially impacts performance if there are many event listeners - consider using direct injections of
     *    dependencies instead.
     * Usage:
     * 1. Define an event On EventManager:
     * public class ScoreUpdatedEvent
     * {
     *  public int NewScore { get; }
     * }
     * 2. Define a listener On controller:
     * public class UIController
     * {
     *  void OnScoreUpdated(ScoreUpdatedEvent eventData)
     *  {
     *      * Do something on score update. *
     *  }
     * }
     * 3. Subscribe to an event using event type and listener:
     * void OnEnable()
     * {
     *  EventSystem.Subscribe<ScoreUpdatedEvent>(OnScoreUpdated);
     * }
     * 4. Unsubscribe from an event using event type and listener:
     * void OnDisable()
     * {
     * EventSystem.Unsubscribe<ScoreUpdatedEvent>(OnScoreUpdated);
     * }
     * 5. Publish an event:
     * public class ScoreManager : MonoBehavior
     * {
     *  public void UpdateScore (int newScore)
     * var newScoreEvent = new ScoreUpdatedEvent(newScore);
     * *or*
     * var newScoreEvent = new ScoreUpdatedEvent { NewScore = newScore };
     * EventSystem.Publish(newScoreEvent);
     * }
     */
    public static class EventManager
    {
        private static readonly Dictionary<Type, Delegate> EventListeners = new Dictionary<Type, Delegate>();

        public class ScoreChangedEvent
        {
            public int NewScore { get; }

            public ScoreChangedEvent(int newScore)
            {
                NewScore = newScore;
            }
        }

        public class HealthChangedEvent
        {
            public int NewHealth { get; }

            public HealthChangedEvent(int newHealth)
            {
                NewHealth = newHealth;
            }
        }

        public class StartPlacingTowerEvent
        {
        }

        public class GameStateChangedEvent
        {
            public IGameState NewGameState { get; }

            public GameStateChangedEvent(IGameState newGameState)
            {
                NewGameState = newGameState;
            }
        }

        public class PlayerPositionChangedEvent
        {
            public Vector3 NewPosition { get; }

            public PlayerPositionChangedEvent(Vector3 newPosition)
            {
                NewPosition = newPosition;
            }
        }

        public class LevelCompletedEvent
        {
            public int LevelNumber { get; }

            public LevelCompletedEvent(int levelNumber)
            {
                LevelNumber = levelNumber;
            }
        }

        public class EnemyDefeatedEvent
        {
            public EnemyBase Enemy { get; }

            public EnemyDefeatedEvent(EnemyBase enemy)
            {
                this.Enemy = enemy;
            }
        }

        public class DialogStartedEvent
        {
            public string DialogName { get; }

            public DialogStartedEvent(string dialogName)
            {
                DialogName = dialogName;
            }
        }

        public class DialogEndedEvent
        {
        }

        public static void Subscribe<T>(Action<T> listener)
        {
            Type eventType = typeof(T);
            EventListeners.TryAdd(eventType, null);
            EventListeners[eventType] = Delegate.Combine(EventListeners[eventType], listener);
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            Type eventType = typeof(T);
            if (!EventListeners.TryGetValue(eventType, out var eventListeners))
            {
                Debug.LogWarning($"EventListener of type {eventType} was not found. Make sure to " +
                                 $"Register it first before trying to unregister it.");
                return;
            }

            eventListeners = Delegate.Remove(eventListeners, listener);
            if (eventListeners == null)
            {
                EventListeners.Remove(eventType);
                return;
            }

            EventListeners[eventType] = eventListeners;
        }

        /** Publishes an event if it exists, else throws an exception. */
        public static void Publish<T>(T eventArgs)
        {
            Type eventType = typeof(T);
            if (!EventListeners.ContainsKey(eventType))
            {
                throw new KeyNotFoundException($"EventListener of type {eventType} was not found." +
                                               $" Make sure to Register it first before trying to access it");
            }

            ((Action<T>)EventListeners[eventType])?.Invoke(eventArgs);
        }
    }
}