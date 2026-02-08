using UnityEngine;

public class PlayerWeaponRotation : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - _spawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _spawnPoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}