using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Events.Publishers;
using _SecondGameJam.Scripts.Gameplay.Both.Combat;
using _SecondGameJam.Scripts.Utilities.UI;
using UnityEngine;

namespace _SecondGameJam.Scripts.UI.Characters.Towers
{
    /** Condition - The object's first child is the Sprite of the object. */
    public class TowerSpawnUI : MonoBehaviour
    {
        [SerializeField] private float _unplacedTowerAlpha = 0.5f;
        private void OnEnable()
        {
            TowerEventPublisher.OnTowerSelected += OnTowerSelected;
            TowerEventPublisher.OnTowerMoved += OnTowerMoved;
            TowerEventPublisher.OnTowerPlaced += OnTowerPlaced;
        }
        
        private void OnDisable()
        {
            TowerEventPublisher.OnTowerSelected -= OnTowerSelected;
            TowerEventPublisher.OnTowerMoved -= OnTowerMoved;
            TowerEventPublisher.OnTowerPlaced -= OnTowerPlaced;
        }

        private void OnTowerSelected(GameObject tower)
        {
            var spriteRendererTransform = tower.transform.GetChild(0);
            var towerSpriteRenderer = spriteRendererTransform.GetComponent<SpriteRenderer>();
            Color towerColor = Color.red.WithAlpha(_unplacedTowerAlpha);
            towerSpriteRenderer.color = towerColor;
        }

        private void OnTowerMoved(GameObject tower, Vector2 newPosition)
        {
            bool canPlaceTower = TowerSpawnManager.Instance.CanPlaceTower(newPosition);
            var spriteRendererTransform = tower.transform.GetChild(0);
            var towerSpriteRenderer = spriteRendererTransform.GetComponent<SpriteRenderer>();
            Color towerColor = canPlaceTower ? Color.gray : Color.red;
            towerColor = towerColor.WithAlpha(_unplacedTowerAlpha);
            towerSpriteRenderer.color = towerColor;
        }

        private void OnTowerPlaced(GameObject tower)
        {
            var spriteRendererTransform = tower.transform.GetChild(0);
            var towerSpriteRenderer = spriteRendererTransform.GetComponent<SpriteRenderer>();
            towerSpriteRenderer.color = Color.white.WithAlpha(1.0f);
            if (!tower.GetComponent<KamikazeTower>())
            {
                var rangeRendererTransform = tower.transform.GetChild(1).GetChild(1);
                var rangeRenderer = rangeRendererTransform.GetComponent<SpriteRenderer>();
                rangeRenderer.enabled = false;
            }

            tower.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            AudioManager.Instance.PlayAudio("Water");
        }
    }
}