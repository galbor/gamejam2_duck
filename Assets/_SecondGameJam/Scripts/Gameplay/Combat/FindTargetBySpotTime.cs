using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both
{
    public class FindClosest : MonoBehaviour
    {
        [SerializeField] private string _targetTag = "Tower";

        public string TargetTag => _targetTag;

        private readonly List<Transform> _targets = new();
        private Transform _transform;


        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.up = _targets.Count > 0 ? _targets[0].position - _transform.position : _transform.parent.up;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TargetTag))
            {
                _targets.Add(other.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TargetTag))
            {
                _targets.Remove(other.transform);
            }
        }

        public Transform GetTarget()
        {
            Transform target = _targets.Count > 0 ? _targets[0] : null;
            return target;
        }
    }
}