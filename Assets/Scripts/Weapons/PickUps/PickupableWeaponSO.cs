using UnityEngine;

[CreateAssetMenu(fileName = "PickupableWeaponSO", menuName = "PickupableWeaponSO")]
public class PickupableWeaponSO : ScriptableObject
{
    // Класс, который содержит информацию о подбираемом оружии

    [SerializeField] private string _weaponTag;
    [SerializeField] private int _waveIndex;

    public string WeaponTag => _weaponTag;
    public int WaveIndex => _waveIndex;
}