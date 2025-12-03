
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class BoxerCombatAI : ShooterCombatAI
    {
        [SerializeField] private Animator _animator;
        private static readonly int TriggerHash = Animator.StringToHash("Attack");

        protected override void Attack()
        {
            if (!_targetStats) return;
            Reorient();
            UpdateAttackTime();
            _animator.SetTrigger(TriggerHash);
            StartCoroutine(ShootCoroutine());
        }
    }
} 