using System.Collections;
using UnityEngine;

public class OnStartScaleAnimation : MonoBehaviour
{
    [SerializeField] private UniversalAnimator _animator;
    [SerializeField] private float _duration = 0.4f;
    [SerializeField] private float _overshootFactor = 1.25f;
    [SerializeField] private float _delay = 0.1f;

    [SerializeField] private Vector3 _startScale = Vector3.zero;
    private Vector3 _targetScale;

    private void Awake()
    {
        _targetScale = transform.localScale;
        transform.localScale = _startScale;
    }

    private void Start()
    {
        if(_delay == 0f)
        {
            _animator.AnimateScaleWithOvershoot(Vector3.zero, _targetScale, _duration, _overshootFactor);
        }
        else
        {
            StartCoroutine(AnimationWithDelay());
        }
    }

    private IEnumerator AnimationWithDelay()
    {
        yield return new WaitForSeconds(_delay);
        _animator.AnimateScaleWithOvershoot(Vector3.zero, _targetScale, _duration, _overshootFactor);
    }
}