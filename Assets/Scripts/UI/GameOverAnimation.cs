using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundHandler;
    [SerializeField] private SpriteRenderer _backgroundImage;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private GameObject _gameOverUIMenuHandler;

    [SerializeField] private float _backgroundTargetAlpha = 0.7f;
    [SerializeField] private float _backgroundFadeDuration = 1.5f;
    [SerializeField] private float _textTypingSpeed = 0.15f;
    [SerializeField] private float _delayBeforeText = 0.5f;
    [SerializeField] private float _delayBeforeButtons = 0.3f;
    [SerializeField] private float _buttonSlideDuration = 0.4f;
    [SerializeField] private float _buttonSlideOffset = 500f;
    [SerializeField] private float _delayBetweenButtons = 0.15f;
    [SerializeField] private Color _backgroundStartColor = new Color(0f, 0f, 0f, 0f);
    [SerializeField] private Color _textColor = Color.red;
    [SerializeField] private string _gameOverString = "Game Over";

    private Vector2[] _buttonTargetPositions;

    private void Start()
    {
        GlobalFlags.Instance.OnGameOver += Play;
    }

    private void OnDestroy()
    {
        GlobalFlags.Instance.OnGameOver -= Play;
    }

    public void Play()
    {
        _backgroundHandler.SetActive(true);
        _gameOverUIMenuHandler.SetActive(true);

        _backgroundImage.color = _backgroundStartColor;
        _gameOverText.text = "";
        _gameOverText.color = _textColor;

        CacheButtonPositions();
        HideButtons();

        StartCoroutine(AnimationSequence());
    }

    private void CacheButtonPositions()
    {
        _buttonTargetPositions = new Vector2[_buttons.Length];
        for (int i = 0; i < _buttons.Length; i++)
            _buttonTargetPositions[i] = _buttons[i].anchoredPosition;
    }

    private void HideButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].anchoredPosition = _buttonTargetPositions[i] + Vector2.down * _buttonSlideOffset;
    }

    private IEnumerator AnimationSequence()
    {
        yield return StartCoroutine(FadeBackground());
        yield return new WaitForSecondsRealtime(_delayBeforeText);
        yield return StartCoroutine(TypeText());
        yield return new WaitForSecondsRealtime(_delayBeforeButtons);
        yield return StartCoroutine(SlideButtons());
    }

    private IEnumerator FadeBackground()
    {
        float elapsed = 0f;
        Color startColor = _backgroundStartColor;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, _backgroundTargetAlpha);

        while (elapsed < _backgroundFadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / _backgroundFadeDuration;
            _backgroundImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        _backgroundImage.color = endColor;
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < _gameOverString.Length; i++)
        {
            _gameOverText.text += _gameOverString[i];
            yield return new WaitForSecondsRealtime(_textTypingSpeed);
        }
    }

    private IEnumerator SlideButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            StartCoroutine(SlideButton(_buttons[i], _buttonTargetPositions[i]));
            yield return new WaitForSecondsRealtime(_delayBetweenButtons);
        }
    }

    private IEnumerator SlideButton(RectTransform button, Vector2 targetPosition)
    {
        Vector2 startPosition = button.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < _buttonSlideDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / _buttonSlideDuration;
            float eased = 1f - (1f - t) * (1f - t);
            button.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, eased);
            yield return null;
        }

        button.anchoredPosition = targetPosition;
    }
}