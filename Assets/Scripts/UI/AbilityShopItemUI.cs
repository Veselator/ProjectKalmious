using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityShopItemUI : MonoBehaviour
{
    // Класс визуального отображения предмета в магазине
    // Нарушение SRP

    [SerializeField] private AvailableAbilitiesManager _availableAbilitiesManager;
    [SerializeField] private int _linkedAbilityId;
    [SerializeField] private string _title;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _button;

    [SerializeField] private Color _affordableColor = Color.white;
    [SerializeField] private Color _notAffordableColor = Color.gray;
    [SerializeField] private Color _purchasedColor = Color.green;

    private AbilitySO _currentAbility;
    private bool _purchased;

    public AbilitySO CurrentAbility => _currentAbility;

    private void Start()
    {
        _currentAbility = _availableAbilitiesManager.GetAbility(_linkedAbilityId);
        _button.onClick.AddListener(OnTryToBuy);
        _availableAbilitiesManager.OnPointsChanged += HandlePointsChanged;

        _titleText.text = _title;
        _priceText.text = _currentAbility.PointsToUnlock.ToString();
        _iconImage.sprite = _currentAbility.Icon;

        UpdateVisual();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnTryToBuy);
        _availableAbilitiesManager.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        if (!_purchased)
            UpdateVisual();
    }

    private void OnTryToBuy()
    {
        if (_purchased) return;

        if (_availableAbilitiesManager.TryToBuy(_linkedAbilityId))
        {
            _purchased = true;
            _button.interactable = false;
            _button.image.color = _purchasedColor;
        }
    }

    private void UpdateVisual()
    {
        bool affordable = _availableAbilitiesManager.IsAffordable(_linkedAbilityId);
        _button.interactable = _purchased ? false : affordable;
        _button.image.color = affordable ? _affordableColor : _notAffordableColor;
    }
}