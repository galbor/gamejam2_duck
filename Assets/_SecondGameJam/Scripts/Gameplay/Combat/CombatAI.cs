using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    [RequireComponent(typeof(CharacterStats))]
    public class CombatAI : MonoBehaviour
    {
        // Target transform is changed by the FindClosest script:
        [SerializeField] protected Transform _targetTransform;
        [SerializeField] private float _speed = 3.0f;
        [SerializeField] private bool _canRotate = true;

        private float _lastAttackTime;
        protected FindClosest _targeter;
        protected CharacterStats _targetStats;
        protected CharacterStats _thisStats;
        private Transform _nextTarget; //tmp variable for finding new target

        protected void UpdateAttackTime() => _lastAttackTime = Time.time;
        protected bool CanAttack() => Time.time - _lastAttackTime >= AttackCooldown;

        // Keep protected field for serialization:
        [SerializeField] protected float _attackCooldown = 2.0f;
        protected float AttackCooldown => _attackCooldown;

        /** Initialize the stats of both the target and the enemy. */
        protected void Start()
        {
            if (_targetTransform != null)
            {
                _targetStats = _targetTransform.GetComponent<CharacterStats>();
            }

            _thisStats = GetComponent<CharacterStats>();
            _targeter = GetComponentInChildren<FindClosest>();
        }

        /** Update target and attack if possible. */
        protected virtual void Update()
        {
            _nextTarget = _targeter.GetTarget();
            if (_nextTarget != _targetTransform)
            {
                _targetTransform = _nextTarget;
                _targetStats = _nextTarget ? _targetTransform.GetComponent<CharacterStats>() : null;
            }

            if (CanAttack())
            {
                Attack();
            }
        }

        /** Moves the enemy towards the target. */
        private void ChaseTarget()
        {
            Transform enemy = transform;
            Vector3 enemyPosition = enemy.position;
            Vector3 direction = (_targetTransform.position - enemyPosition).normalized;
            enemyPosition += direction * (_speed * Time.deltaTime);
            enemy.position = enemyPosition;
        }

        /** Attacks the target if it has a CharacterStats component. */
        protected virtual void Attack()
        {
            if (!_targetStats) return;

            Reorient();
            UpdateAttackTime();
            _targetStats.TakeDamage(_thisStats.Attack);
        }

        protected void Reorient()
        {
            if (!_canRotate || gameObject.CompareTag("Enemy")) return;
            transform.right = -_targetTransform.position + transform.position;
        }
    }
}