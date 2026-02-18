using UnityEngine;

public class ScaleIfAvailable : MonoBehaviour
{
    [SerializeField] private float _pulseSpeed = 2f;
    [SerializeField] private float _pulseAmount = 0.1f;

    private INotifyAvailabilityChanged _availability;
    private Vector3 _originalScale;
    private bool _isPulsing;

    private void Start()
    {
        _originalScale = transform.localScale;
        _availability = GetComponent<INotifyAvailabilityChanged>();

        if (_availability.State) HandleTrue();

        _availability.OnTrue += HandleTrue;
        _availability.OnFalse += HandleFalse;
    }

    private void OnDestroy()
    {
        _availability.OnTrue -= HandleTrue;
        _availability.OnFalse -= HandleFalse;

        _isPulsing = false;
    }

    private void Update()
    {
        if (!_isPulsing) return;

        float t = Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmount;
        transform.localScale = _originalScale * (1f + t);
    }

    private void HandleTrue()
    {
        _isPulsing = true;
    }

    private void HandleFalse()
    {
        _isPulsing = false;
        transform.localScale = _originalScale;
    }
}