using UnityEngine;

public class FixedPointsCameraTracker : MonoBehaviour, ICameraTracker
{
    // Камера интерполируется между позициями
    [SerializeField] protected Transform[] _points;
    protected Vector3 _targetPosition;

    [Header("То, насколько плавно будет движение камеры")]
    [SerializeField] protected float _speed = 5f;

    private void Start()
    {
        _targetPosition = _points[0].position;
    }

    public void SetTarget(int id)
    {
        if (id < 0 || id >= _points.Length) return;

        _targetPosition = _points[id].position;
    }

    public virtual Vector3 GetCurrentPosition(Vector3 _)
    {
        return Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}
