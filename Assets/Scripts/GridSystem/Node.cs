using UnityEngine;

public class Node : IHeapItem<Node>
{
    public int X;
    public int Y;
    public Vector3 WorldPosition;
    public bool Walkable;

    public int GCost;
    public int HCost;
    public Node Parent;
    public int FCost => GCost + HCost;
    public int HeapIndex { get; set; }

    public Node(Vector3 worldPos, int x, int y, bool walkable) 
    {
        X = x;
        Y = y;
        Walkable = walkable;
        WorldPosition = worldPos;
    }

    public int CompareTo(Node nodeToCompare) 
    {
        var compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare != 0) return -compare;
        compare = HCost.CompareTo(nodeToCompare.HCost);
        return -compare;
    }
}