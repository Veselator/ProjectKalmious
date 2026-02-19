using UnityEngine;

[CreateAssetMenu(fileName = "PlayerVisuals", menuName = "Game/Player Visuals")]
public class PlayerVisualsSO : ScriptableObject
{
    [SerializeField] private Sprite[] _visuals;

    public int Count => _visuals != null ? _visuals.Length : 0;

    public Sprite GetVisual(int index)
    {
        if (_visuals == null || index < 0 || index >= _visuals.Length)
            return null;
        return _visuals[index];
    }
}
