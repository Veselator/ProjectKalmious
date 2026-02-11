using UnityEngine;

public class SimpleAI : BaseAI
{
    protected override void UpdateAI()
    {
        if (_player == null || _movement == null) return;

        Vector2 directionToPlayer = GetDirectionToPlayer();
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer > _stoppingDistance)
        {
            _movement.Move(directionToPlayer);
        }
        else
        {
            _movement.Stop();
        }
    }

    private Vector2 GetDirectionToPlayer()
    {
        Vector2 direction = (_player.position - transform.position).normalized;
        return direction;
    }

    public void SetStoppingDistance(float distance)
    {
        _stoppingDistance = distance;
    }

    public float GetDistanceToPlayer()
    {
        if (_player == null) return float.MaxValue;
        return Vector2.Distance(transform.position, _player.position);
    }
}