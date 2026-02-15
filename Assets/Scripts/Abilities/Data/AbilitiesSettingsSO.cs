// AbilitiesSettingsSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilitiesSettings", menuName = "Abilities/AbilitiesSettings")]
public class AbilitiesSettingsSO : ScriptableObject
{
    [SerializeField] private AbilitySO[] _abilities;

    public AbilitySO[] Abilities => _abilities;
}