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
    [SerializeField] private LayerMask _targetLayers;

    public event Action<GameObject> OnProjectileSpawned;
    public event Action OnShoot;

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
            SpawnProjectile(i);
        }
    }

    private void SpawnProjectile(int index)
    {
        Quaternion spawnRotation = _shootPoint.rotation;

        float angleOffset = CalculateSpreadOffset(index);
        spawnRotation *= Quaternion.Euler(0f, 0f, angleOffset);

        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, spawnRotation);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Initialize(_damage, _projectileSpeed, spawnRotation * transform.right, _targetLayers);

        OnProjectileSpawned?.Invoke(projectile);
    }

    private float CalculateSpreadOffset(int index)
    {
        if (_projectilesPerShot == 1) return 0f;

        float step = _spreadAngle / (_projectilesPerShot - 1);
        return -_spreadAngle / 2f + step * index;
    }
}