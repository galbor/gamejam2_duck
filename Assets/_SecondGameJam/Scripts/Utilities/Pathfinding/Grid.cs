using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Pathfinding
{
    /** The entire play-field of the game.
     * Usage: Place on a GameObject in the scene.
     * Reason for MonoBehaviour - Change the grid through the editor.
     */
    public class Grid : MonoBehaviour
    {
        public LayerMask UnwalkableMask;
        public Vector2 GridWorldSize;
        public float NodeRadius;
        private Node[,] _grid;
        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;

        /** Creates the grid. */
        private void Awake()
        {
            _nodeDiameter = 2.0f * NodeRadius;
            // Number of nodes that can fit in the grid.
            _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            Vector2 worldMiddle = Vector2.right * GridWorldSize.x / 2 + Vector2.up * GridWorldSize.y / 2;
            Vector2 worldBottomLeft = (Vector2)transform.position - worldMiddle;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector2 worldXOffset = Vector2.right * (x * _nodeDiameter + NodeRadius);
                    Vector2 worldYOffset = Vector2.up * (y * _nodeDiameter + NodeRadius);
                    Vector2 worldPoint = worldBottomLeft + worldXOffset + worldYOffset;
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, NodeRadius, UnwalkableMask));
                    _grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public Node NodeFromWorldPoint(Vector2 worldPosition)
        {
            float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
            float percentY = (worldPosition.y + GridWorldSize.y / 2) / GridWorldSize.y;
            // Make sure the location is within the grid:
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
            return _grid[x, y];
        }
        
        /** Gets a node's neighbours from 8 directions. */
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    { // Current node:
                        continue;
                    }

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    // If the node is within the grid:
                    if ((checkX >= 0 && checkX < _gridSizeX) && (checkY >= 0 && checkY < _gridSizeY))
                    {
                        neighbours.Add(_grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }
    }
}