using UnityEngine;

public class AddXpPoint : MonoBehaviour
{
    [SerializeField] private float _attractSpeed = 8f;
    [SerializeField] private float _steeringSpeed = 5f;
    [SerializeField] private float _initialSpreadForce = 2f;
    [SerializeField] private float _spreadDamping = 3f;
    private float _xpValue = 1f;

    [SerializeField] private LayerMask _attractRadius;
    [SerializeField] private LayerMask _pickupLayer;

    private Vector2 _direction;
    private Transform _playerTransform;
    private bool _isAttracted;
    private float _currentSpeed;

    private void Start()
    {
        _direction = Random.insideUnitCircle.normalized;
        _currentSpeed = _initialSpreadForce;
    }

    private void Update()
    {
        if (_isAttracted && _playerTransform != null)
        {
            Vector2 toPlayer = ((Vector2)_playerTransform.position - (Vector2)transform.position).normalized;
            _direction = Vector2.Lerp(_direction, toPlayer, _steeringSpeed * Time.deltaTime).normalized;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _attractSpeed, _steeringSpeed * Time.deltaTime);
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, _spreadDamping * Time.deltaTime);
        }

        transform.position += (Vector3)(_direction * _currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInLayer(collision.gameObject.layer, _attractRadius))
        {
            _playerTransform = collision.transform;
            _isAttracted = true;
        }

        if (IsInLayer(collision.gameObject.layer, _pickupLayer))
        {
            GlobalFlags.Instance.TriggerAddXp(_xpValue);
            Destroy(gameObject);
            return;
        }
    }

    private bool IsInLayer(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}