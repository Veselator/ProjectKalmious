// MenuSmoothShowAnimation.cs
using UnityEngine;

public class MenuSmoothShowAnimation : MonoBehaviour, IMenuShowAnimation
{
    [SerializeField] private UniversalAnimator _animator;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _duration = 0.4f;
    private RectTransform _rectTransform;

    private Vector2 _endPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _endPos = _rectTransform.anchoredPosition;
        _rectTransform.anchoredPosition = _startPos;
    }

    public void Play(bool IsShowingAnim)
    {
        if (IsShowingAnim)
            _animator.AnimateAnchoredPosition(_startPos, _endPos, _duration);
        else
            _animator.AnimateAnchoredPosition(_endPos, _startPos, _duration);
    }
}