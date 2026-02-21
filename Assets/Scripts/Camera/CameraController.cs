using UnityEngine;

public class CameraController : MonoBehaviour
{
    private ICameraTracker _tracker;
    [SerializeField] private Transform _target;
    private Transform _defaultTracker;
    public Transform Target
    {
        get => _target;
        set
        {
            _target = value;
        }
    }

    public bool IsAbleToUpdate = true;
    [SerializeField] private Vector3 _defaultTrackingPosition = Vector3.zero;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, -10);

    [SerializeField] private CameraShake _cameraShake;

    private void Start()
    {
        _tracker = GetComponent<ICameraTracker>();
        _defaultTracker = _target;

        if (_cameraShake == null) _cameraShake = GetComponentInChildren<CameraShake>();
    }

    public void ResetTrackingObject()
    {
        _target = _defaultTracker;
    }

    private void LateUpdate()
    {
        if (!IsAbleToUpdate) return;

        Vector3 newVector = _target != null ? _tracker.GetCurrentPosition(_target.position) + _offset : _tracker.GetCurrentPosition(_defaultTrackingPosition) + _offset;
        transform.position = new Vector3(newVector.x, newVector.y, _offset.z);

        if (_cameraShake != null)
        {
            ApplyCameraShake();
        }
    }

    private void ApplyCameraShake()
    {
        Vector3 shakeOffset = _cameraShake.ShakeOffset;
        transform.position += shakeOffset;
    }
}