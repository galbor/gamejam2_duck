using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class DamageAura : MonoBehaviour
    {
        [SerializeField] private string _targetTag;
        public string TargetTag { get => _targetTag; set => _targetTag = value;}
        public int Damage { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TargetTag))
            {
                other.GetComponent<CharacterStats>()?.TakeDamage(Damage);
            }
        }
    }
}