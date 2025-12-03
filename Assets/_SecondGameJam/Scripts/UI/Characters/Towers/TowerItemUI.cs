using System.Collections.Generic;
using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;
using UnityEngine.UI;

namespace _SecondGameJam.Scripts.UI.Characters.Towers
{
    /** Usage: Attach to an empty GameObject with a button component. */
    [RequireComponent(typeof(Button))]
    public class TowerItemUI : MonoBehaviour
    {
        [SerializeField] private int _maxStack = 5;
        private Button _button;
        private Image _icon;
        private readonly List<Image> _towerStack = new();
        // Set tower config from TowerInventoryUI using Initialize() method.
        private TowerConfig _config;

        private void OnEnable()
        {
            TowerInventoryEventPublisher.OnInventoryUpdated += UpdateUI;
        }

        private void OnDisable()
        {
            TowerInventoryEventPublisher.OnInventoryUpdated -= UpdateUI;
        }

        /** Set a TowerItem's config and count after creation. */
        public void Initialize(TowerConfig config, int count)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _config = config;
            CreateTowerStack(config);
            UpdateUI(_config, count);
        }

        private void CreateTowerStack(TowerConfig config)
        {
            for (int i = 0; i < _maxStack; i++)
            {
                Transform child = transform.GetChild(i); 
                Image towerImage = child.GetComponent<Image>();
                towerImage.sprite = config.Icon;
                _towerStack.Add(towerImage);
            }
        }

        private void OnClick()
        {
            AudioManager.Instance.PlayAudio("Click");
            TowerSpawnManager.Instance.SelectTower(_config);
        }

        /** Set TowerItem's count after each inventory update. */
        public void UpdateUI(TowerConfig tower, int count)
        {
            if (tower == _config)
            {
                for (int i = 0; i < _maxStack; i++)
                {
                    _towerStack[i].gameObject.SetActive(i < count);
                }
                _button.interactable = count > 0;
            }
        }
    }
}