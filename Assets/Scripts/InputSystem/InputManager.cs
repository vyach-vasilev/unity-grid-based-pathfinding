using System;
using UnityEngine;

public class InputManager: MonoBehaviour
{
    public static InputManager Instance;
    public event Action<Vector3> OnMoved;
    
   // public Vector3 Position { get; set; }
    private void Awake()
    {
        Instance = this;
    }
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
            if (Physics.Raycast(ray, out var hit))
            {
                var isGridExist = hit.transform.TryGetComponent<GridController>(out _);
            
                if (!isGridExist)
                {
                    return;
                }

                var point = hit.point;
                var map = GridMap.Instance;
                var position = map.NodeFromWorldPoint(point).WorldPosition;
                OnMoved?.Invoke(position);
            }
        }
    }
}