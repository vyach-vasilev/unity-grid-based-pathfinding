using UnityEngine;

public class GridGizmosDrawer: MonoBehaviour
{
    private Color _pathColor = new(0,0.75f,0.5f,0.5f);
    private Color _walkableColor = new(1,1,1,0.5f);
    private Color _unWalkableColor = new(1,0,0,0.5f);
    
    [SerializeField] private Vector2Int _gridSize = new(10,10);

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(_gridSize.x,1,_gridSize.y));
        var map = GridMap.Instance;
        
        if (map == null)
        {
            return;
        }
        
        var nodes = map.Nodes;
        if (nodes != null)
        {
            foreach (Node n in nodes) 
            {
                Gizmos.color = (n.Walkable)?_walkableColor:_unWalkableColor;
                Gizmos.DrawCube(n.WorldPosition + Vector3.down * 0.25f, new Vector3(0.8f,0.5f,0.8f));
            }
        }
    }
}