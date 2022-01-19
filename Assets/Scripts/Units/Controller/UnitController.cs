using System.Threading.Tasks;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private InputManager _inputManager;
    private int _targetIndex;

    [SerializeField] private float _speed = 10;
    [SerializeField] private Vector3[] _path;
    
    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _inputManager.OnMoved += MakeMove;
    }

    private void OnDestroy()
    {
        _inputManager.OnMoved -= MakeMove;
    }

    private void MakeMove(Vector3 targetPosition)
    {
        PathRequestManager.RequestPath(transform.position, targetPosition, OnPathFound);
    }
    
    private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            _path = newPath;
            _targetIndex = 0;
            FollowPath();
        }
    }

    private async void FollowPath()
    {
        Vector3 currentWaypoint = _path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                _targetIndex++;
                if (_targetIndex >= _path.Length)
                {
                    break;
                }
                currentWaypoint = _path[_targetIndex];
            }

            var targetDir = currentWaypoint - transform.position;
            var step = _speed * Time.deltaTime;
            var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, _speed * Time.deltaTime);

            await Task.Yield();
        }
    }

    public void OnDrawGizmos()
    {
        if (_path != null)
        {
            for (int i = _targetIndex; i < _path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(_path[i] - Vector3.down * 0.25f, new Vector3(0.8f,0.5f,0.8f));
                Gizmos.DrawLine(i == _targetIndex ? transform.position : _path[i - 1], _path[i]);
            }
        }
    }
}