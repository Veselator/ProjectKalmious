using UnityEngine;
using System;

public class ProjectileWeapon : BaseWeapon
{
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
        Vector3 spawnPosition = _shootPoint != null ? _shootPoint.position : transform.position;
        Quaternion spawnRotation = _shootPoint != null ? _shootPoint.rotation : transform.rotation;

        if (_projectilesPerShot > 1)
        {
            float angleOffset = CalculateSpreadOffset(index);
            spawnRotation *= Quaternion.Euler(0f, 0f, angleOffset);
        }

        GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, spawnRotation);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.Initialize(_damage, _projectileSpeed, spawnRotation * Vector3.right, _targetLayers);
        }

        OnProjectileSpawned?.Invoke(projectile);
    }

    private float CalculateSpreadOffset(int index)
    {
        if (_projectilesPerShot == 1) return 0f;

        float step = _spreadAngle / (_projectilesPerShot - 1);
        return -_spreadAngle / 2f + step * index;
    }
}