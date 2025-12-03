using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class FreezerCombatAI : CombatAI
    {
        [SerializeField] private FreezeRay _freezeRay;

        // The time in seconds it takes to freeze an enemy.
        // Thawing is done by the enemy.
        [SerializeField] private float _freezeRate = 1f;

        private new void Start()
        {
            base.Start();
            _freezeRay.FreezeRate = _freezeRate;
            _freezeRay.Damage = _thisStats.Attack;
            _freezeRay.TargetTag = _targeter.TargetTag;
            _freezeRay.DamageCooldown = AttackCooldown;
        }

        protected override void Update()
        {
            _targetTransform = _targeter.GetTarget();

            if (_targetTransform)
            {
                Reorient();
                Attack();
            }
            else
            {
                Holster();
            }
        }

        // TODO: A different attack implies an interface.
        protected override void Attack()
        {
            _freezeRay.gameObject.SetActive(true);
        }

        private void Holster()
        {
            _freezeRay.gameObject.SetActive(false);
        }
    }
}