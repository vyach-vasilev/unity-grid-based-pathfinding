using UnityEngine;

public class GridFactory
{
    public void Create(out GridMap map, int width, int height, Vector3 position, float nodeRadius, LayerMask unwalkableMask)
    {
        map = new GridMap(new Vector2(width, height), position, nodeRadius, unwalkableMask);
    }
}