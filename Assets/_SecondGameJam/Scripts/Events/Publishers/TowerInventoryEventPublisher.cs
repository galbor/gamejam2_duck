using _SecondGameJam.Scripts.Data.ScriptableObjects;

namespace _SecondGameJam.Scripts.Events.Publishers
{
    public static class TowerInventoryEventPublisher
    {
        public delegate void InventoryUpdatedEvent(TowerConfig towerConfig, int count);

        public delegate void InventoryReplacedEvent(TowerInventory towerInventory);

        public static event InventoryUpdatedEvent OnInventoryUpdated;
        public static event InventoryReplacedEvent OnInventoryReplaced;

        public static void UpdateInventory(TowerConfig towerConfig, int count)
        {
            OnInventoryUpdated?.Invoke(towerConfig, count);
        }

        public static void ReplaceInventory(TowerInventory towerInventory)
        {
            OnInventoryReplaced?.Invoke(towerInventory);
        }
    }
}