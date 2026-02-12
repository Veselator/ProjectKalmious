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
        transform.LookAt(_player);
    }

    private void HandleInited(BaseAI _, Transform player)
    {
        _player = player;
        _isInited = true;
    }
}
