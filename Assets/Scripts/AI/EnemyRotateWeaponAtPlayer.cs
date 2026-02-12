using UnityEngine;

public class EnemyRotateWeaponAtPlayer : MonoBehaviour
{
    [SerializeField] private BaseAI _ai;
    private Transform _player;
    private bool _isInited = false;

    private void Awake()
    {
        _ai.OnInited += HandleInited;
    }

    private void OnDestroy()
    {
        _ai.OnInited -= HandleInited;
    }

    private void Update()
    {
        if (!_isInited) return;
        Vector2 direction = _player.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleInited(BaseAI _, Transform player)
    {
        _player = player;
        _isInited = true;
    }
}
