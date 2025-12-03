using System.Collections.Generic;
using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.UI.Characters.Towers
{
    public class TowerInventoryUI : MonoBehaviour
    {
        [SerializeField] private TowerItemUI _towerItemPrefab;
        [SerializeField] private Transform _towerItemContainer;
        // TODO: Create a TowerInventory class to store all towers in the game.
        // TODO: Only towers from the first level should have a count > 0.
        [SerializeField] private TowerInventory _allTowers;
        private readonly Dictionary<TowerConfig, TowerItemUI> _configToTowerItemUI = new();

        private void Awake()
        {
            PopulateInventory();
        }

        private void PopulateInventory()
        {
            foreach (var tower in _allTowers.AvailableTowers)
            {
                TowerItemUI itemUI = Instantiate(_towerItemPrefab, _towerItemContainer);
                itemUI.Initialize(tower.Config, tower.Count);
                itemUI.gameObject.SetActive(tower.Count > 0);
                _configToTowerItemUI[tower.Config] = itemUI;
            }
        }

        private void OnEnable()
        {
            TowerInventoryEventPublisher.OnInventoryReplaced += OnInventoryReplaced;
        }
        
        private void OnDisable()
        {
            TowerInventoryEventPublisher.OnInventoryReplaced -= OnInventoryReplaced;
        }

        private void OnInventoryReplaced(TowerInventory levelInventory)
        {
            foreach (var (towerConfig, itemUI) in _configToTowerItemUI)
            {
                int count = levelInventory.GetTowerCount(towerConfig);
                itemUI.UpdateUI(towerConfig, count);
                itemUI.gameObject.SetActive(count > 0);
            }
            AudioManager.Instance.PlayAudio("Bell");
        }
    }
}