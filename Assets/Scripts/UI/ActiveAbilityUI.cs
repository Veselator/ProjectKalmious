using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _filledImage;
    [SerializeField] private Image _active;

    private IAbility _ability;

    public void Initialize(IAbility ability, Sprite icon)
    {
        _ability = ability;
        _icon.sprite = icon;
        _filledImage.fillAmount = 0f;
        _active.enabled = true;

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

    private void OnDestroy()
    {
        if (_ability == null) return;
        _ability.Cooldown.OnStart -= HandleCooldownStart;
        _ability.Cooldown.OnTick -= HandleCooldownTick;
        _ability.Cooldown.OnEnd -= HandleCooldownEnd;
    }
}