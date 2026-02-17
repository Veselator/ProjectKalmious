using UnityEngine;

public class FlipSpriteOnDirectionChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _linkedSprite;
    [SerializeField] private RigidbodyMovement _linkedMovement;

    private void OnEnable()
    {
        _linkedMovement.OnDirectionChanged += HandleDirectionChanged;
    }

    private void OnDisable()
    {
        _linkedMovement.OnDirectionChanged -= HandleDirectionChanged;
    }

    private void HandleDirectionChanged(Vector2 newDirection)
    {
        if (newDirection.x > 0.01f)
        {
            _linkedSprite.flipX = false;
        }
        else if (newDirection.x < -0.01f)
        {
            _linkedSprite.flipX = true;
        }
    }
}
