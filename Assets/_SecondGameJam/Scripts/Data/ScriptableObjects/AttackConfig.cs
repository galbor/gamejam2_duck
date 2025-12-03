using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** The stats of an attack. */
    [CreateAssetMenu(fileName = "AttackConfig", menuName = "Character/Attack Config")]
    public class AttackConfig : ScriptableObject
    {
        public int Damage;
        public float AttackSpeed;
        public float Range;
        public float AttackCooldown;
    }
}