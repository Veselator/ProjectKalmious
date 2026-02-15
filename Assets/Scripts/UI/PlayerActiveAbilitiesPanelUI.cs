using UnityEngine;

public class PlayerActiveAbilitiesPanelUI : MonoBehaviour
{
    [SerializeField] private AbilitiesManager _abilitiesManager;
    [SerializeField] private GameObject _abilityCellPrefab;

    private void OnEnable()
    {
        _abilitiesManager.OnAbilityAdded += HandleAbilityAdded;
    }

    private void OnDisable()
    {
        _abilitiesManager.OnAbilityAdded -= HandleAbilityAdded;
    }

    private void HandleAbilityAdded(IAbility ability, GameObject abilityObject, int slotIndex)
    {
        AbilitySO abilitySO = ability.AbilityData;
        GameObject cell = Instantiate(_abilityCellPrefab, transform);

        ActiveAbilityUI cellUI = cell.GetComponent<ActiveAbilityUI>();
        cellUI.Initialize(ability, abilitySO.Icon, _abilitiesManager, slotIndex);
    }
}