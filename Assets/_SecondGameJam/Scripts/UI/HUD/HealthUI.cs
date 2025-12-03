using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.UI.HUD
{
    public class HealthUI : MonoBehaviour
    {
        private void OnEnable()
        {
            HealthEventPublisher.OnPlayerTakeDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            HealthEventPublisher.OnPlayerTakeDamage -= OnTakeDamage;
        }
        
        private void OnTakeDamage(float healthPercent)
        {
            AudioManager.Instance.PlayAudio("Duck", volume: 0.3f);
        }
    }
}