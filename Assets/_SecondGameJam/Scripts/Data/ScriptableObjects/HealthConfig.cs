using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** The stats of a character's health. */
    [CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Character/Health Config")]
    public class HealthConfig : ScriptableObject
    {
        public int CurrentHealth;
        public int MaxHealth;
        public float DefensePercentage;
    }
}