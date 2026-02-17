using UnityEngine;
using UnityEngine.UI;

public class PointerUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _enemySprite;
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private Color _enemyColor = Color.red;
    [SerializeField] private Color _weaponColor = Color.yellow;

    private Transform _target;
    private Transform _playerTransform;
    private BaseAI _linkedAI;
    private float _orbitRadius;
    private float _hideDistance;
    private bool _isEnemy;
    private bool _isActive;

    public bool IsActive => _isActive;
    public Transform Target => _target;

    public void Initialize(BaseAI enemy, Transform playerTransform, float orbitRadius, float hideDistance)
    {
        _linkedAI = enemy;
        _target = enemy.transform;
        _playerTransform = playerTransform;
        _orbitRadius = orbitRadius;
        _hideDistance = hideDistance;
        _isEnemy = true;
        _isActive = true;

        _icon.sprite = _enemySprite;
        _icon.color = _enemyColor;
        gameObject.SetActive(true);
    }

    public void Initialize(Transform weaponTransform, Transform playerTransform, float orbitRadius, float hideDistance)
    {
        _linkedAI = null;
        _target = weaponTransform;
        _playerTransform = playerTransform;
        _orbitRadius = orbitRadius;
        _hideDistance = hideDistance;
        _isEnemy = false;
        _isActive = true;

        _icon.sprite = _weaponSprite;
        _icon.color = _weaponColor;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _isActive = false;
        _target = null;
        _linkedAI = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isActive || _target == null || _playerTransform == null)
            return;

        float distance = _isEnemy
            ? _linkedAI.DistanceToPlayer
            : Vector2.Distance(_target.position, _playerTransform.position);

        if (distance <= _hideDistance)
        {
            _icon.enabled = false;
            return;
        }

        _icon.enabled = true;

        Vector2 direction = (_target.position - _playerTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);

        float x = Mathf.Cos(angle) * _orbitRadius;
        float y = Mathf.Sin(angle) * _orbitRadius;
        transform.localPosition = new Vector3(x, y, 0f);

        float rotationZ = angle * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}