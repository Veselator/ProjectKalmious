using UnityEngine;

public class OnStartPositionAnimation : MonoBehaviour
{
    [SerializeField] private UniversalAnimator _animator;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _duration = 0.4f;

    private Vector2 _targetPos;

    private void Awake()
    {
        _targetPos = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().anchoredPosition = _startPos;
    }

    private void Start()
    {
        _animator.AnimateAnchoredPosition(_startPos, _targetPos, _duration);
    }
}