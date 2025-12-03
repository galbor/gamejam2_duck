using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    [System.Serializable]
    public class CharacterStats : MonoBehaviour
    {
        // Use _maxHealth for serialization purposes:
        [SerializeField] private int _maxHealth = 100;
        public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

        public int CurrentHealth { get; private set; }
        [SerializeField] private int _attackPower = 10;
        public int Attack { get => _attackPower; private set => _attackPower = value; }
        private HealthBar _healthBar;

        private bool IsAlive => CurrentHealth > 0;


        private void Start()
        {
            CurrentHealth = MaxHealth;
            _healthBar = GetComponentInChildren<HealthBar>();
        }

        public void TakeDamage(int damage)
        {
            if (!IsAlive) return;
            CurrentHealth -= damage;
            if (_healthBar) _healthBar.UpdateHealthBar(MaxHealth, CurrentHealth);
            if (gameObject.CompareTag("Tower") || gameObject.CompareTag("Doctor"))
            {
                float healthPercent = (float)CurrentHealth / MaxHealth;
                HealthEventPublisher.TakeDamage(healthPercent);
            }

            if (!IsAlive)
            {
                KillCharacter();
            }
        }

        private void KillCharacter()
        {
            gameObject.SetActive(false);
            if (gameObject.CompareTag("Enemy"))
            {
                DeathEventPublisher.EnemyDeath();
            }
        }

        /** Heals character only if health amount is positive. */
        public void Heal(int amount)
        {
            if (!IsAlive) return;
            if (amount < 0)
            {
                Debug.LogError($"Heal amount cannot be negative. Got: {amount}");
            }

            CurrentHealth += amount;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
            _healthBar.UpdateHealthBar(MaxHealth, CurrentHealth);
        }
    }
}