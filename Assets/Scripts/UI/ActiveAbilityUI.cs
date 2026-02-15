using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _filledImage;
    [SerializeField] private Image _active;
    [SerializeField] private TMP_Text _numberText;

    private int _slotIndex;

    private AbilitiesManager _abilitiesManager;

    private IAbility _ability;

    public void Initialize(IAbility ability, Sprite icon, AbilitiesManager am, int index)
    {
        _ability = ability;
        _icon.sprite = icon;
        _filledImage.fillAmount = 0f;
        _active.enabled = true;
        _numberText.text = (index + 1).ToString();

        _abilitiesManager = am;
        _slotIndex = index;

        _ability.Cooldown.OnStart += HandleCooldownStart;
        _ability.Cooldown.OnTick += HandleCooldownTick;
        _ability.Cooldown.OnEnd += HandleCooldownEnd;
    }

    private void HandleCooldownStart()
    {
        _active.enabled = false;
        _filledImage.fillAmount = 1f;
    }

    private void HandleCooldownTick(float progress)
    {
        _filledImage.fillAmount = 1f - progress;
    }

    private void HandleCooldownEnd()
    {
        _active.enabled = true;
        _filledImage.fillAmount = 0f;
    }

    public void TryToApplyAbility()
    {
        _abilitiesManager.UseAbility(_slotIndex);
    }

    private void OnDestroy()
    {
        if (_ability == null) return;
        _ability.Cooldown.OnStart -= HandleCooldownStart;
        _ability.Cooldown.OnTick -= HandleCooldownTick;
        _ability.Cooldown.OnEnd -= HandleCooldownEnd;
    }
}