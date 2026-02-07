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

        // Ищем CameraShake в дочерних объектах
        if (_cameraShake == null) _cameraShake = GetComponentInChildren<CameraShake>();
    }

    public void ResetTrackingObject()
    {
        // Для кат-сцен
        _target = _defaultTracker;
    }

    private void LateUpdate()
    {
        if (!IsAbleToUpdate) return;

        if (_target != null)
            transform.position = _tracker.GetCurrentPosition(_target.position) + _offset;
        else
            transform.position = _tracker.GetCurrentPosition(_defaultTrackingPosition) + _offset;

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