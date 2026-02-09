using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelVisual : MonoBehaviour
{
    [SerializeField] private Image _progressImage;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _xpText;
    [SerializeField] private PlayerLevelHandler _level;

    private void OnEnable()
    {
        _level.OnLevelChanged += UpdateLevelText;
        _level.OnXPChanged += UpdateXPVisual;
    }

    private void OnDisable()
    {
        _level.OnLevelChanged -= UpdateLevelText;
        _level.OnXPChanged -= UpdateXPVisual;
    }

    private void Start()
    {
        UpdateLevelText(_level.CurrentLevel);
        UpdateXPVisual(_level.CurrentXP, _level.CurrentXPGoal);
    }

    private void UpdateLevelText(int level)
    {
        _levelText.text = $"Level {level}";
    }

    private void UpdateXPVisual(float currentXP, float xpGoal)
    {
        _progressImage.fillAmount = currentXP / xpGoal;
        _xpText.text = $"{Mathf.FloorToInt(currentXP)}/{Mathf.FloorToInt(xpGoal)}";
    }
}