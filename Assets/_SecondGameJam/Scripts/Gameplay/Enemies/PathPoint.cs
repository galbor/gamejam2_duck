using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Enemies
{
    public class PathPoint : MonoBehaviour
    {
        [SerializeField] private PathPoint[] _nextPoints;

        public PathPoint GetNextPoint()
        {
            if (_nextPoints.Length == 0)
            {
                return null;
            }

            return _nextPoints[Random.Range(0, _nextPoints.Length)];
        }
    }
}