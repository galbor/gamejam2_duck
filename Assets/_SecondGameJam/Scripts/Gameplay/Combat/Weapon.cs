using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class Weapon : MonoBehaviour
    {
        // Keep private field for serialization:
        [SerializeField] private string _targetTag;
        public string TargetTag { get => _targetTag; set => _targetTag = value; }

        [SerializeField] private int _damage = 1;
        public int Damage { get => _damage; set => _damage = value; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_targetTag))
            {
                DamageEnemy(other.gameObject);
            }
        }

        protected void DamageEnemy(GameObject other)
        {
            other.gameObject.GetComponent<CharacterStats>()?.TakeDamage(Damage);
        }
    }
}