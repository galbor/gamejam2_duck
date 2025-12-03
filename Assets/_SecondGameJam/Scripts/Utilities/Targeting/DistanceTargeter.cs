using UnityEngine;

namespace _SecondGameJam.Scripts.Utilities.Targeting
{
    /** Finds the closest target to tower.
     * Usage: Place on a TowerTargeter/EnemyTargeter object and set the target layer and tag.
     */
    public class DistanceTargeter
    {
        [SerializeField] private float _detectRadius = 5.0f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private string _targetTag;
        
        /** Get */
        public GameObject GetTarget(Transform attacker)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(attacker.position, _detectRadius,
                                                              _targetLayer);
            GameObject target = null;
            float minDistance = Mathf.Infinity;
            foreach (var currentTarget in targets)
            {
                if (!currentTarget.CompareTag(_targetTag))
                {
                    continue;
                }
                float distance = Vector2.Distance(attacker.position, currentTarget.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = currentTarget.gameObject;
                }
            }
            return target;
        } }
}