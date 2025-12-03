using System.Collections.Generic;
using _SecondGameJam.Scripts.Gameplay.Enemies;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class FreezeRay : Weapon
    {
        // The time it takes to freeze an enemy in seconds.
        // Thawing is done by the enemy itself.
        public float FreezeRate { get; set; }
        
        public float DamageCooldown { get; set; } = 0.5f;
        
        private readonly Dictionary<GameObject, EnemyBase> _enemyCache = new();
        private readonly HashSet<GameObject> _enemiesInRay = new();


        // TODO: Regular update suffices.
        private void FixedUpdate()
        {
            // Create a copy of the set to avoid concurrent modification:
            var affectedEnemies = new HashSet<GameObject>(_enemiesInRay);
            foreach (var enemy in affectedEnemies)
            {
                if (_enemyCache.TryGetValue(enemy, out var enemyComponent))
                {
                    enemyComponent.Freeze(FreezeRate * Time.fixedDeltaTime);
                }

                // Damage frozen enemies:
                if (DamageCooldown < 0)
                {
                    DamageEnemy(enemy);
                }
            }
            DamageCooldown = DamageCooldown < 0 ? 0 : DamageCooldown - Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TargetTag))
            {
                if (!_enemyCache.ContainsKey(other.gameObject))
                {
                    _enemyCache.Add(other.gameObject, other.gameObject.GetComponent<EnemyBase>());
                }

                _enemiesInRay.Add(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _enemiesInRay.Remove(other.gameObject);
        }
    }
}