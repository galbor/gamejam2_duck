using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class ProximityMine : MonoBehaviour
    {
        private const string BORDER_TAG = "border";
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private string _targetTag;
    
        private ParticleDamage _explosion;
        private CharacterStats _thisStats;
        private ObjectPoolManager _objectPoolManager;
        
        private void Awake()
        {
            _objectPoolManager = GameObject.Find(_explosionPrefab.GetComponent<ParticleDamage>().ObjectPoolManagerName)
                .GetComponent<ObjectPoolManager>();
            _thisStats = GetComponentInParent<CharacterStats>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_targetTag))
            {
                Explode();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(BORDER_TAG))
            {
                Explode();
            }
        }

        private void Explode()
        {
            var explosionGO = _objectPoolManager.GetObjectFromPool();
            explosionGO.transform.position = transform.position;
            _explosion = explosionGO.GetComponent<ParticleDamage>();
        
            _explosion.Damage = _thisStats.Attack;
            _explosion.TargetTag = _targetTag;
            _explosion.ObjectPoolManager = _objectPoolManager;
            _explosion.Explode();
            _thisStats.TakeDamage(10000);
        }
    }
}
