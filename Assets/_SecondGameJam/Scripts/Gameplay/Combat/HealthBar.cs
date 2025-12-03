using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _SecondGameJam.Scripts.Gameplay.Both
{
    [RequireComponent(typeof(Image))]
    public class HealthBar : MonoBehaviour
    {
        private Image _image;
        private float _target = 1f;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 parentPosition = transform.parent.transform.parent.position; 
            transform.parent.position = parentPosition + new Vector3(0, 0.7f, 0); 
        }  

        public void UpdateHealthBar(float maxHealth, float currentHealth)
        {
            _target = currentHealth / maxHealth;
            _image.fillAmount = _target;
        }
    }
}