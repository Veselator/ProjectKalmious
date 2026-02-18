using UnityEngine;

public class ChangeColorIfAvailable : MonoBehaviour
{
    [SerializeField] private UniversalAnimator _animator;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _cycleSpeed = 1f;

    private INotifyAvailabilityChanged _availability;
    private bool _isAnimating;
    private float _gradientTime;

    private Color _inactiveColor;

    private void Start()
    {
        _inactiveColor = _animator.GetCurrentColor();
        _availability = GetComponent<INotifyAvailabilityChanged>();

        if (_availability.State) HandleTrue();

        _availability.OnTrue += HandleTrue;
        _availability.OnFalse += HandleFalse;
    }

    private void OnDestroy()
    {
        _availability.OnTrue -= HandleTrue;
        _availability.OnFalse -= HandleFalse;
        _isAnimating = false;
    }

    private void Update()
    {
        if (!_isAnimating) return;

        _gradientTime += Time.deltaTime * _cycleSpeed;
        float t = Mathf.PingPong(_gradientTime, 1f);
        Color color = _gradient.Evaluate(t);

        _animator.SetColor(color);
    }

    private void HandleTrue()
    {
        _gradientTime = 0f;
        _isAnimating = true;
    }

    private void HandleFalse()
    {
        _isAnimating = false;
        _animator.InterpolateColor(_inactiveColor, _duration);
    }
}