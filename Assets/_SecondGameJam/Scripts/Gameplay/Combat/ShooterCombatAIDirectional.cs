using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class ShooterCombaiAIDirectional : ShooterCombatAI
    {
        [SerializeField] private float[] _attackDirections;
        [SerializeField] private string _targetTag;
        [SerializeField] private bool _isMultipleAttack = true;

        private int _curDirection;

        protected override void Update()
        {
            if (CanAttack())
            {
                UpdateAttackTime();
                Attack();
            }
        }

        protected override void Attack()
        {
            if (_isMultipleAttack)
            {
                for (_curDirection = 0; _curDirection < _attackDirections.Length; _curDirection++)
                {
                    Shoot(_attackDirections[_curDirection], _targetTag);
                }
            }
            else
            {
                Shoot(_attackDirections[_curDirection], _targetTag);
                _curDirection = (_curDirection + 1) % _attackDirections.Length;
            }
        }
    }
}