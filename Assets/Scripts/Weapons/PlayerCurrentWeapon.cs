using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerCurrentWeapon : MonoBehaviour
{
    // Инициализирует все оружия
    // А также контроллирует текущее

    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private Transform _weaponSpawnPoint;

    public static PlayerCurrentWeapon Instance { get; private set; }

    private Dictionary<string, GameObject> _instantiatedWeapons;
    private IWeapon _currentWeapon;
    private GameObject _currentWeaponObject;

    public IWeapon CurrentWeapon => _currentWeapon;
    public List<IWeapon> AllWeapons => GetAllInstantiatedWeapons();

    public event Action<IWeapon> OnWeaponChanged;

    public event Action OnInited;
    public bool IsInited { get; private set; } = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        _instantiatedWeapons = new Dictionary<string, GameObject>();
        Instance = this;
    }

    public void Init()
    {
        InitializeAllWeapons();
        SubscribeToInventory();
        UpdateCurrentWeapon(_inventory.CurrentPointer);
    }

    private void OnDestroy()
    {
        UnsubscribeFromInventory();
    }

    private void InitializeAllWeapons()
    {
        WeaponInventoryItemSO[] allWeapons = GlobalWeapons.Instance.GetAllWeapons();

        foreach (WeaponInventoryItemSO weaponItem in allWeapons)
        {
            if (weaponItem.Prefab != null && !_instantiatedWeapons.ContainsKey(weaponItem.Name))
            {
                GameObject weaponObject = Instantiate(weaponItem.Prefab, _weaponSpawnPoint);
                weaponObject.SetActive(false);
                _instantiatedWeapons[weaponItem.Name] = weaponObject;
            }
        }

        IsInited = true;
        OnInited?.Invoke();
    }

    private void SubscribeToInventory()
    {
        _inventory.OnItemAdded += UpdateCurrentWeapon;
        _inventory.OnCurrentSlotChanged += UpdateCurrentWeapon;
    }

    private void UnsubscribeFromInventory()
    {
        _inventory.OnItemAdded -= UpdateCurrentWeapon;
        _inventory.OnCurrentSlotChanged -= UpdateCurrentWeapon;
    }

    public List<IWeapon> GetAllInstantiatedWeapons()
    {
        List<IWeapon> weapons = new List<IWeapon>();

        foreach (var kvp in _instantiatedWeapons)
        {
            weapons.Add(kvp.Value.GetComponent<IWeapon>());
        }

        return weapons;
    }
    public GameObject GetWeaponObjectByTag(WeaponInventoryItemSO item)
    {
        return GetWeaponObjectByTag(item.Name);
    }

    public GameObject GetWeaponObjectByTag(string tag)
    {
        if (!_instantiatedWeapons.ContainsKey(tag)) return null;
        return _instantiatedWeapons[tag];
    }

    private void UpdateCurrentWeapon(WeaponInventoryItemSO _, int id)
    {
        UpdateCurrentWeapon(id);
    }

    private void UpdateCurrentWeapon(int slotIndex)
    {
        if (_currentWeaponObject != null)
        {
            _currentWeaponObject.SetActive(false);
        }

        WeaponInventoryItemSO selectedItem = _inventory.GetItem(slotIndex);

        if (selectedItem != null && _instantiatedWeapons.TryGetValue(selectedItem.Name, out GameObject weaponObject))
        {
            _currentWeaponObject = weaponObject;
            _currentWeaponObject.SetActive(true);
            _currentWeapon = _currentWeaponObject.GetComponent<IWeapon>();

            OnWeaponChanged?.Invoke(_currentWeapon);
        }
        else
        {
            _currentWeaponObject = null;
            _currentWeapon = null;
        }
    }

    public void UseCurrentWeapon()
    {
        if (_currentWeapon != null && _currentWeapon.CanAct())
        {
            _currentWeapon.Act();
        }
    }
}