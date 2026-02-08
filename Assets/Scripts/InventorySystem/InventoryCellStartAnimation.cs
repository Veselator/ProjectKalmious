using UnityEngine;
using System.Collections;

public class InventoryCellStartAnimation : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _overshoot = 1.3f;
    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector3 _targetScale;

    private void Awake()
    {
        _targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Play()
    {
        StartCoroutine(PlayAnimation());
    }

    public void Play(float delay)
    {
        StartCoroutine(PlayAnimationWithDelay(delay));
    }

    private IEnumerator PlayAnimationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return PlayAnimation();
    }

    private IEnumerator PlayAnimation()
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / _duration;
            float curveValue = _curve.Evaluate(progress);

            float overshootValue = curveValue;
            if (progress < 0.5f)
            {
                overshootValue = Mathf.Lerp(0f, _overshoot, curveValue * 2f);
            }
            else
            {
                overshootValue = Mathf.Lerp(_overshoot, 1f, (curveValue - 0.5f) * 2f);
            }

            transform.localScale = _targetScale * overshootValue;
            yield return null;
        }

        transform.localScale = _targetScale;
    }
}