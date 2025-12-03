using UnityEngine;

namespace _SecondGameJam.Scripts.Events.Publishers
{
    /** Used to subscribe to tower events.
     * Usage:
     * 1. Subscription(OnEnabled) - TowerEventPublisher.OnTowerPlaced += OnTowerPlaced;
     * 2. Unsubscription(OnDisabled) - TowerEventPublisher.OnTowerPlaced -= OnTowerPlaced;
     * 3. Event invocation - towerEventPublisher.OnTowerPlaced?.Invoke();
     */
    public static class TowerEventPublisher
    {
        public delegate void TowerSelectedEvent(GameObject tower);

        public delegate void TowerMovedEvent(GameObject tower, Vector2 newPosition);

        public delegate void TowerPlacedEvent(GameObject tower);

        public static event TowerSelectedEvent OnTowerSelected;
        public static event TowerMovedEvent OnTowerMoved;
        public static event TowerPlacedEvent OnTowerPlaced;

        public static void SelectTower(GameObject tower)
        {
            OnTowerSelected?.Invoke(tower);
        }

        public static void MoveTower(GameObject tower, Vector2 newPosition)
        {
            OnTowerMoved?.Invoke(tower, newPosition);
        }

        public static void PlaceTower(GameObject tower)
        {
            OnTowerPlaced?.Invoke(tower);
        }
    }
}