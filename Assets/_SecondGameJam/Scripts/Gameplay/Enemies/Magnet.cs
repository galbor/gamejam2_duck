using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Enemies
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private float _pullForce = 1;
        [SerializeField] private string[] _targetTags;

        private bool ArrayContainsTag(GameObject other)
        {
            foreach (var targetTag in _targetTags)
            {
                if (other.CompareTag(targetTag)) return true;
            }

            return false;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
            if (ArrayContainsTag(other.gameObject))
            {
                Vector2 direction = (transform.position - other.transform.position).normalized;
                other.attachedRigidbody.AddForce(direction * _pullForce);
            }
        }
    }
}