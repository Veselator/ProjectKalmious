using UnityEngine;

public class StaticRegen : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _regenAmount = 2f;

    private void Update()
    {
        if (_health.IsDamaged)
        {
            _health.CurrentHealth += _regenAmount * Time.deltaTime;
        }
    }
}
