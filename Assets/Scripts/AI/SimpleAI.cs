using UnityEngine;

public class SimpleAI : BaseAI
{
    protected override void UpdateAI()
    {
        if (_player == null || _movement == null) return;

        Vector2 directionToPlayer = GetDirectionToPlayer();

        if (DistanceToPlayer > _stoppingDistance)
        {
            _movement.Move(directionToPlayer);
        }
        else
        {
            _movement.Stop();
        }
    }
}