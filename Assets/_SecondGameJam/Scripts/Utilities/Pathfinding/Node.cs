using UnityEngine;

namespace _SecondGameJam.Scripts.Pathfinding
{
    /** A single area unit in a grid. */
    public class Node
    {
        public Node Parent;
        public bool IsWalkable;
        public Vector2 WorldPosition;
        public int GridX, GridY;
        /** GCost - cost from start to current node.
         * HCost - Estimated cost from current node to target.
         */
        public int GCost, HCost;
        public int FCost => GCost + HCost;
        
        public Node(bool isWalkable, Vector2 worldPosition, int gridX, int gridY)
        {
            IsWalkable = isWalkable;
            WorldPosition = worldPosition;
            (GridX , GridY) = (gridX, gridY);
        }
    }
}