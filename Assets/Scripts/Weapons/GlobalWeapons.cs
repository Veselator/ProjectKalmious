using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GlobalWeapons : MonoBehaviour
{
    // Нужно получить все оружия в игре?
    // Или конккретное по тегу?

    // Всегда пожалуйста! Этот класс обеспечит

    [SerializeField] private WeaponsConfigSO _config;

    private Dictionary<string, WeaponInventoryItemSO> _weaponsCache;

    public static GlobalWeapons Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeCache();
    }

    private void InitializeCache()
    {
        _weaponsCache = new Dictionary<string, WeaponInventoryItemSO>();

        foreach (var entry in _config.LinkedItems)
        {
            if (!string.IsNullOrEmpty(entry.Name))
            {
                _weaponsCache[entry.Name] = entry;
            }
        }
    }

    public WeaponInventoryItemSO[] GetAllWeapons()
    {
        return _weaponsCache.Values.ToArray();
    }

    public GameObject GetWeaponByTag(string tag)
    {
        if (_weaponsCache.TryGetValue(tag, out WeaponInventoryItemSO item))
        {
            return item.Prefab;
        }

        return null;
    }

    public WeaponInventoryItemSO GetWeaponItemByTag(string tag)
    {
        if (_weaponsCache.TryGetValue(tag, out WeaponInventoryItemSO item))
        {
            return item;
        }

        return null;
    }
}