using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using _SecondGameJam.Scripts.Gameplay.Both.Combat;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    public class TowerSpawnManager : MonoBehaviour
    {
        public static TowerSpawnManager Instance { get; private set; }
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _towerContainer;
        [SerializeField] private LayerMask _placementLayerMask;
        [SerializeField] private LayerMask _enemyLayerMask;
        private TowerConfig _selectedTowerConfig;
        private GameObject _selectedTower;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            LevelEventPublisher.OnLevelChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            LevelEventPublisher.OnLevelChanged -= OnLevelChanged;
        }

        private void OnLevelChanged(LevelConfig levelConfig)
        {
            ClearTowers();
            _selectedTowerConfig = null;
            _selectedTower = null;
        }

        private void ClearTowers()
        {
            for (int i = _towerContainer.childCount - 1; i >= 0; i--)
            {
                var child = _towerContainer.GetChild(i);
                var childObj = child.gameObject;
                Destroy(childObj);
            }
        }

        /** Place Towers on left mouse button click. */
        private void Update()
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (!_selectedTower) return;
            _selectedTower.transform.position = mousePosition;
            TowerEventPublisher.MoveTower(_selectedTower, mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (CanPlaceTower(mousePosition))
                {
                    PlaceTower(_selectedTower);
                }
            }
        }

        public void SelectTower(TowerConfig selectedTower)
        {
            if (_selectedTower)
            {
                Destroy(_selectedTower);
            }

            _selectedTowerConfig = selectedTower;
            AssetManager.Instance.LoadAssetAsync<GameObject>(selectedTower.PrefabAddress, ShowSelectedTower);
        }

        /** Displays tower after load. */
        private void ShowSelectedTower(GameObject towerPrefab)
        {
            _selectedTower = Instantiate(towerPrefab, _towerContainer);
            TowerEventPublisher.SelectTower(_selectedTower);
            ToggleTowerCombatAI(false);
        }

        private void ToggleTowerCombatAI(bool enable)
        {
            var combatComponent = _selectedTower.GetComponent<CombatAI>();
            if (combatComponent) combatComponent.enabled = enable;
            var proximityMineComponent = _selectedTower.GetComponent<ProximityMine>();
            if (proximityMineComponent) proximityMineComponent.enabled = enable;
            var collider2d = _selectedTower.GetComponent<Collider2D>();
            collider2d.enabled = enable;
            if (_selectedTowerConfig.Name == "Kamikaze")
            {
                var rangeRender = _selectedTower.transform.GetChild(1).GetChild(0);
                var rangeCollider = rangeRender.GetComponent<CircleCollider2D>();
                rangeCollider.enabled = enable;
            }
        }

        public bool CanPlaceTower(Vector2 mousePosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, _placementLayerMask);
            bool isCorrectLayer = hit.collider;
            Collider2D obstacle = Physics2D.OverlapCircle(mousePosition, 0.5f, _enemyLayerMask);
            bool isNotOverlapping = !obstacle;
            return isCorrectLayer && isNotOverlapping;
        }

        private void PlaceTower(GameObject towerPrefab)
        {
            ToggleTowerCombatAI(true);
            TowerInventoryManager.Instance.DecrementTowerCount(_selectedTowerConfig);
            TowerEventPublisher.PlaceTower(towerPrefab);
            _selectedTowerConfig = null;
            _selectedTower = null;
        }
    }
}