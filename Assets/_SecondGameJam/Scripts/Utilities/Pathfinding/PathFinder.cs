using System;
using System.Collections.Generic;
using _SecondGameJam.Scripts.Pathfinding;
using UnityEngine;
using Grid = _SecondGameJam.Scripts.Pathfinding.Grid;

namespace _SecondGameJam.Scripts.Utilities.Pathfinding
{
    /** Finds a movement path using A* algorithm. */
    public class PathFinder : MonoBehaviour
    {
        private const int MOVEMENT_COST = 10;
        private readonly int DIAGONAL_MOVEMENT_COST = Mathf.RoundToInt(MOVEMENT_COST * Mathf.Sqrt(2));
        private Grid _grid;
        // Path for debugging:
        private List<Node> _path = new();

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        public List<Node> FindPath(Vector2 startPos, Vector2 targetPos)
        {
            Node startNode = _grid.NodeFromWorldPoint(startPos);
            Node targetNode = _grid.NodeFromWorldPoint(targetPos);

            List<Node> openSet = new() { startNode };
            HashSet<Node> closedSet = new();

            while (openSet.Count > 0)
            {
                var currentNode = GetClosestNode(openSet);
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                if (currentNode == targetNode)
                {
                    // Path found:
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node neighbour in _grid.GetNeighbours(currentNode))
                {
                    if (neighbour.IsWalkable && !closedSet.Contains(neighbour))
                    {
                        UpdateNeighbourDist(currentNode, neighbour, targetNode, openSet);
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
            return null; // Can't reach goal.
        }
        
        private static Node GetClosestNode(IReadOnlyList<Node> openSet)
        {
            Node closestNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < closestNode.FCost ||
                    openSet[i].FCost == closestNode.FCost && openSet[i].HCost < closestNode.HCost)
                {
                    closestNode = openSet[i];
                }
            }

            return closestNode;
        }

        private void UpdateNeighbourDist(Node currentNode, Node nextNode, Node targetNode, 
                                         ICollection<Node> openSet)
        {
            int newCostToNeighbour = currentNode.GCost + d(currentNode, nextNode);
            if (newCostToNeighbour < nextNode.GCost || !openSet.Contains(nextNode))
            {
                nextNode.GCost = newCostToNeighbour;
                nextNode.HCost = d(nextNode, targetNode);
                nextNode.Parent = currentNode;
            }
        }

        /** Calculates shortest way from A to B. */
        private int d(Node nodeA, Node nodeB)
        {
            int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (distX > distY)
            {
                return DIAGONAL_MOVEMENT_COST * distY + MOVEMENT_COST * (distX - distY);
            }

            return DIAGONAL_MOVEMENT_COST * distX + MOVEMENT_COST * (distY - distX);
        }

        private List<Node> RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new();
            Node currentNode = targetNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            _path = path; // Line for debugging.
            return path;
        }

        /** Draws the found path if exists. */
        private void OnDrawGizmos()
        {
            if (_grid != null && _path != null)
            {
                foreach (Node n in _path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (2*_grid.NodeRadius - 0.1f));
                }
            }
        }
    }
}