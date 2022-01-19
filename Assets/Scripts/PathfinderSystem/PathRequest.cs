using System;
using UnityEngine;

public struct PathRequest
{
    public Vector3 PathStart;
    public Vector3 PathEnd;
    public Action<Vector3[], bool> Callback;

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback) 
    {
        PathStart = start;
        PathEnd = end;
        Callback = callback;
    }
}