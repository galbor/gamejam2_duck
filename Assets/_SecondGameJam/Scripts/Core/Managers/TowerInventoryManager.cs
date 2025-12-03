using System.Collections.Generic;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /** Keeps track of towers in game.
     * Incorporate UI by subscribing to TowerInventoryEventPublisher events.
     */
    public class TowerInventoryManager : MonoBehaviour
    {
        public static TowerInventoryManager Instance { get; private set; }
        private readonly Dictionary<TowerConfig, int> _towerCounts = new();
        public int GetTowerCount(TowerConfig config) => _towerCounts.GetValueOrDefault(config, 0);

        private void OnEnable()
        {
            LevelEventPublisher.OnLevelChanged += SetLevelInventory;
        }

        private void OnDisable()
        {
            LevelEventPublisher.OnLevelChanged -= SetLevelInventory;
        }


        /** Initializes singleton instance. */
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void SetLevelInventory(LevelConfig level)
        {
            TowerInventory levelInventory = level.TowerInventory;
            SetTowerInventory(levelInventory);
        }

        /** Sets the number of towers in inventory to be exactly {amount} towers. */
        private void SetTowerInventory(TowerInventory towerInventory)
        {
            _towerCounts.Clear();
            foreach (var towerStack in towerInventory.AvailableTowers)
            {
                var config = towerStack.Config;
                var count = towerStack.Count;
                _towerCounts[config] = count;
            }

            TowerInventoryEventPublisher.ReplaceInventory(towerInventory);
        }


        /** Reduces {amount} towers from the inventory. */
        public void DecrementTowerCount(TowerConfig tower)
        {
            if (_towerCounts.ContainsKey(tower))
            {
                int count = Mathf.Max(0, _towerCounts[tower] - 1);
                _towerCounts[tower] = count;
                TowerInventoryEventPublisher.UpdateInventory(tower, count);
            }
            else
            {
                Debug.LogError($"Tower {tower} not found in _towerCounts");
            }
        }
    }
}