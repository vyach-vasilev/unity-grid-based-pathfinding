using System;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager
{
    private static PathRequestManager _instance;

    private readonly Queue<PathRequest> _pathRequestQueue = new();
    private readonly Pathfinder _pathfinder;
    
    private PathRequest _currentPathRequest;
    private bool _isProcessingPath;

    public PathRequestManager(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
        _instance = this;
    }
    
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        var newRequest = new PathRequest(pathStart, pathEnd, callback);
        _instance._pathRequestQueue.Enqueue(newRequest);
        _instance.TryProcessNext();
    }

    private void TryProcessNext()
    {
        if (!_isProcessingPath && _pathRequestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinder.FindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        _currentPathRequest.Callback(path, success);
        _isProcessingPath = false;
        TryProcessNext();
    }
}