using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class ParticleDamage : MonoBehaviour
    {
        [SerializeField] private string _objectPoolManagerName;
        public string ObjectPoolManagerName => _objectPoolManagerName;
        [SerializeField] private ParticleSystem _particleSystem;
    
        private readonly List<ParticleCollisionEvent> _collisionEvents = new();

        [SerializeField] private string _targetTag;
        public string TargetTag { get => _targetTag; set => _targetTag = value;}
        public int Damage { get; set; } = 1;
        public ObjectPoolManager ObjectPoolManager { get; set; }

        private int _particleAmt = 1;

        private void Awake()
        {
            _particleAmt = (int)_particleSystem.emission.GetBurst(0).count.constant;
        }
    

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag(TargetTag))
            {
                CharacterStats stats = other.GetComponent<CharacterStats>();
                _particleSystem.GetCollisionEvents(other, _collisionEvents);
                foreach (var collision in _collisionEvents)
                {
                    int damage = (int)(Damage * collision.intersection.magnitude / _particleAmt);
                    stats.TakeDamage(damage);
                }
            }
        }

        public float GetExplosionDuration()
        {
            return _particleSystem.main.duration;
        }
    
        public void Explode()
        {
            StartCoroutine(ExplosionCoroutine());
        }

        private IEnumerator ExplosionCoroutine()
        {
            _particleSystem.Play();
            yield return new WaitForSeconds(GetExplosionDuration());
            if (ObjectPoolManager) ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
