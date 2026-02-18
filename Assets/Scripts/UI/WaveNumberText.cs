using UnityEngine;
using TMPro;

public class WaveNumberText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UniversalAnimator _animator;
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private Vector3 _sizeFactor = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private Color _peakColor = Color.red;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private string _format = "Wave {0}";

    private Vector3 _originalScale;
    private Color _originalColor;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _originalColor = _text.color;
    }

    private void OnEnable()
    {
        _wavesManager.OnWaveStarted += HandleWaveStarted;
    }

    private void OnDisable()
    {
        _wavesManager.OnWaveStarted -= HandleWaveStarted;
    }

    private void HandleWaveStarted(int waveId)
    {
        _text.text = string.Format(_format, waveId);

        Vector3 peakScale = new Vector3(
            _originalScale.x * _sizeFactor.x,
            _originalScale.y * _sizeFactor.y,
            _originalScale.z * _sizeFactor.z
        );

        transform.localScale = peakScale;
        _animator.AnimateScale(_originalScale, _duration);

        _animator.InterpolateColor(_peakColor, _originalColor, _duration);
    }
}