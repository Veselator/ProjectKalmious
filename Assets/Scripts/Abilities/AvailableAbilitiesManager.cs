using System;
using System.Collections.Generic;
using UnityEngine;

public class AvailableAbilitiesManager : MonoBehaviour
{
    [SerializeField] private AbilitiesSettingsSO _settings;
    [SerializeField] private AbilitiesManager _abilitiesManager;
    [SerializeField] private WavesManager _wavesManager;

    private int _availablePoints;
    private HashSet<string> _purchasedAbilities = new HashSet<string>();

    public int AvailablePoints => _availablePoints;
    public AbilitySO[] AllAbilities => _settings.Abilities;

    public event Action<int> OnPointsChanged;
    public event Action<AbilitySO> OnAbilityPurchased;

    private void OnEnable()
    {
        _wavesManager.OnWaveEnded += HandleWaveEnded;
    }

    private void OnDisable()
    {
        _wavesManager.OnWaveEnded -= HandleWaveEnded;
    }

    private void HandleWaveEnded(int waveId)
    {
        _availablePoints++;
        OnPointsChanged?.Invoke(_availablePoints);
    }

    public AbilitySO GetAbility(int id)
    {
        return _settings.Abilities[id];
    }

    public bool IsAnythingAffordable()
    {
        foreach (var ability in _settings.Abilities)
        {
            if (!_purchasedAbilities.Contains(ability.ID) && _availablePoints >= ability.PointsToUnlock) return true;
        }

        return false;
    }

    public bool IsAffordable(string id)
    {
        AbilitySO ability = FindAbilityById(id);
        if (ability == null) return false;
        return !_purchasedAbilities.Contains(id) && _availablePoints >= ability.PointsToUnlock;
    }

    public bool IsAffordable(int index)
    {
        if (index < 0 || index >= _settings.Abilities.Length) return false;
        return IsAffordable(_settings.Abilities[index].ID);
    }

    public bool TryToBuy(string id)
    {
        if (!IsAffordable(id)) return false;

        AbilitySO ability = FindAbilityById(id);
        _availablePoints -= ability.PointsToUnlock;
        _purchasedAbilities.Add(id);

        _abilitiesManager.AddAbility(ability);

        OnPointsChanged?.Invoke(_availablePoints);
        OnAbilityPurchased?.Invoke(ability);
        return true;
    }

    public bool TryToBuy(int index)
    {
        if (index < 0 || index >= _settings.Abilities.Length) return false;
        return TryToBuy(_settings.Abilities[index].ID);
    }

    private AbilitySO FindAbilityById(string id)
    {
        foreach (var ability in _settings.Abilities)
        {
            if (ability.ID == id)
                return ability;
        }
        return null;
    }
}