using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInventoryItem", menuName = "Inventory/WeaponInventoryItem")]
public class WeaponInventoryItemSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public string Name => _name;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
}