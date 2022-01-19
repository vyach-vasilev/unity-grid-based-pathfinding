using System.Collections.Generic;
using UnityEngine;

public class GridMap
{
    public static GridMap Instance;

    private readonly LayerMask _unWalkableMask;
    private readonly float _nodeRadius;
    private readonly Vector2 _mapSize;
    private readonly float _nodeDiameter;
    private readonly int _gridSizeX;
    private readonly int _gridSizeY;

    public Node[,] Nodes { get; private set; }
    public List<Node> Path { get; set; }
    public int MaxSize => _gridSizeX * _gridSizeY;
    
    public GridMap(Vector2 mapSize, Vector3 position, float nodeRadius, LayerMask unWalkableMask)
    {
        _unWalkableMask = unWalkableMask;
        _nodeRadius = nodeRadius;
        _nodeDiameter = _nodeRadius * 2;
        _mapSize = mapSize;
        _gridSizeX = Mathf.RoundToInt(_mapSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_mapSize.y / _nodeDiameter);
        CreateGrid(position);

        Instance = this;
    }

    private void CreateGrid(Vector3 position)
    {
        Nodes = new Node[_gridSizeX, _gridSizeY];
        var worldBottomLeft = position - Vector3.right * _mapSize.x / 2 - Vector3.forward * _mapSize.y / 2;
        for (var x = 0; x < _gridSizeX; x++)
        for (var y = 0; y < _gridSizeY; y++)
        {
            var worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
            var walkable = !Physics.CheckSphere(worldPoint, _nodeRadius, _unWalkableMask);
            Nodes[x, y] = new Node(worldPoint, x, y, walkable);
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        var percentX = (worldPosition.x + _mapSize.x / 2) / _mapSize.x;
        var percentY = (worldPosition.z + _mapSize.y / 2) / _mapSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        var x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        var y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return Nodes[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        var neighbours = new List<Node>();

        for (var x = -1; x <= 1; x++)
        for (var y = -1; y <= 1; y++)
        {
            if (x == 0 && y == 0)
                continue;
            
            if (x == 1 && y == 1 || x == -1 && y == -1 || x == 1 && y == -1 || x == -1 && y == 1)
                continue;

            var checkX = node.X + x;
            var checkY = node.Y + y;

            if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                neighbours.Add(Nodes[checkX, checkY]);
        }

        return neighbours;
    }
}