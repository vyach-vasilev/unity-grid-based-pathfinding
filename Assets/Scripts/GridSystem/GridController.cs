using UnityEngine;

public class GridController : MonoBehaviour
{
    private GridMap _map;
    private Pathfinder _pathfinder;
    private GridFactory _gridFactory;
    private InputManager _inputManager;
    private PathRequestManager _pathRequestManager;
    
    [SerializeField] private LayerMask _unwalkableMask;
    [SerializeField] private float _nodeRadius = 1;
    [SerializeField] private Vector2Int _gridSize = new(10,10);
    
    private void Awake()
    {
        _gridFactory = new GridFactory();
        _gridFactory.Create(out _map, _gridSize.x, _gridSize.y, transform.position, _nodeRadius, _unwalkableMask);
        _inputManager = gameObject.AddComponent<InputManager>();
        _pathfinder = gameObject.AddComponent<Pathfinder>();
        _pathfinder.Setup(_map);
    }
}
