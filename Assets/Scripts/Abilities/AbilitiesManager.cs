using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [SerializeField] private Transform _ownerTransform;
    [SerializeField] private Collider2D _ownerCollider;
    [SerializeField] private int _maxSlots = 4;

    private List<IAbility> _abilities = new List<IAbility>();

    public IReadOnlyList<IAbility> Abilities => _abilities;
    public int MaxSlots => _maxSlots;

    public event Action<IAbility, int> OnAbilityAdded;
    public event Action<int> OnAbilityUsed;

    public bool AddAbility(AbilitySO abilitySO)
    {
        if (_abilities.Count >= _maxSlots) return false;

        GameObject abilityObject = Instantiate(abilitySO.Prefab, _ownerTransform);
        IAbility ability = abilityObject.GetComponent<IAbility>();
        ability.Initialize(_ownerTransform, this, _ownerCollider);

        _abilities.Add(ability);
        OnAbilityAdded?.Invoke(ability, _abilities.Count - 1);
        return true;
    }

    public void UseAbility(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _abilities.Count)
            return;

        IAbility ability = _abilities[slotIndex];
        if (ability.CanDo())
        {
            ability.Do();
            OnAbilityUsed?.Invoke(slotIndex);
        }
    }
}