using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
public class WeaponsConfigSO : ScriptableObject
{
    public WeaponInventoryItemSO[] LinkedItems;
}