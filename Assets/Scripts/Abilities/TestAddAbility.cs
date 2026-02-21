using UnityEngine;

public class TestAddAbility : MonoBehaviour
{
    [SerializeField] private AbilitiesManager _abilitiesManager;
    [SerializeField] private AbilitySO[] _abilitiesToAdd;

    private void Start()
    {
        foreach (var ability in _abilitiesToAdd)
        {
            _abilitiesManager.AddAbility(ability);
        }
    }
}
