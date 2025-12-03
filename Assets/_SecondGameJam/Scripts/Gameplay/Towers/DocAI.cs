using System;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class DocAI : CombatAI
    {
        protected override void Attack()
        {
            // TODO: Change inheritance to interface, as base class is not used.
            if (!_targetStats) return;
            int missingHealth = _targetStats.MaxHealth - _targetStats.CurrentHealth;
            if (missingHealth > 0)
            {
                UpdateAttackTime();
                int maxHealAmount = Math.Min(missingHealth, _thisStats.CurrentHealth);
                int healAmount = Math.Min(_thisStats.Attack, maxHealAmount);
                _targetStats.Heal(healAmount);
                _thisStats.TakeDamage(healAmount);
            }
        }
    }
}