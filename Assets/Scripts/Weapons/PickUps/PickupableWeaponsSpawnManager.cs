using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupableWeaponsSpawnManager : MonoBehaviour
{
    // Отвечает за спавн подбираемых оружий
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private GameObject _pickupPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private List<PickupableWeaponSO> _pickupableWeapons;

    private GlobalWeapons _globalWeapons;

    private void Start()
    {
        _globalWeapons = GlobalWeapons.Instance;

        _wavesManager.OnWaveStarted += CheckWave;

        _pickupableWeapons.OrderBy(w => w.WaveIndex);
    }

    private void OnDestroy()
    {
        _wavesManager.OnWaveStarted -= CheckWave;
    }

    private void CheckWave(int waveId)
    {
        WeaponInventoryItemSO weapon;
        foreach (var pickup in _pickupableWeapons)
        {
            if (waveId < pickup.WaveIndex) return;
            if(waveId > pickup.WaveIndex) continue;

            weapon = _globalWeapons.GetWeaponItemByTag(pickup.WeaponTag);
            if(weapon == null) continue;

            SpawnWeapon(weapon);
        }
    }

    // Да, нарушение SRP
    private void SpawnWeapon(WeaponInventoryItemSO weapon)
    {
        Vector3 randomPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        GameObject pickupObject = Instantiate(_pickupPrefab, randomPosition, Quaternion.identity);
        pickupObject.GetComponent<IPickupableWeapon>().Initialize(weapon, _playerInventory);
    }
}
