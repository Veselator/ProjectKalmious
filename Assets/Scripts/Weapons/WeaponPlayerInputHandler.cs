using UnityEngine;

public class WeaponPlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerCurrentWeapon _weaponManager;

    private IWeaponInputStrategy _inputStrategy;

    private void Awake()
    {
        _inputStrategy = new MouseClickWeaponStrategy();

        if (_weaponManager == null)
        {
            _weaponManager = GetComponent<PlayerCurrentWeapon>();
        }
    }

    private void Update()
    {
        _inputStrategy.HandleInput(_weaponManager);
    }

    public void SetInputStrategy(IWeaponInputStrategy strategy)
    {
        _inputStrategy = strategy;
    }
}