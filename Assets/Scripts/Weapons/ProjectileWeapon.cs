using UnityEngine;
using System;

public class ProjectileWeapon : BaseWeapon
{
    // Оружие, которое стреляет пульками
    // Или стрелами
    // Или мочой
    // Ну короче, на что хватит фантазии и нервов арт-дизайнера

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private int _projectilesPerShot = 1;
    [SerializeField] private float _spreadAngle = 0f;
    [SerializeField] private int _spreadStep = 5;
    [SerializeField] private LayerMask _targetLayers;

    public event Action<GameObject> OnProjectileSpawned;
    public event Action OnShoot;

    private int _projectileIndex = 0;

    public override void Act()
    {
        if (!CanAct()) return;

        StartAct();
        Shoot();
        CompleteAct();
    }

    private void Shoot()
    {
        OnShoot?.Invoke();

        for (int i = 0; i < _projectilesPerShot; i++)
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        Quaternion spawnRotation = _shootPoint.rotation;

        if(_spreadAngle != 0f)
        {
            spawnRotation *= Quaternion.Euler(0f, 0f, CalculateSpreadOffset(_projectileIndex));
        }

        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, spawnRotation);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Initialize(_damage, _projectileSpeed, spawnRotation * Vector3.right, _targetLayers);

        OnProjectileSpawned?.Invoke(projectile);

        // Обновляем индекс проджектайла
        _projectileIndex = (_projectileIndex + 1) % _spreadStep;
    }

    private float CalculateSpreadOffset(int index)
    {
        float half = _spreadAngle / 2f;
        float step = _spreadAngle / _spreadStep;
        return -half + step * index;
    }
}