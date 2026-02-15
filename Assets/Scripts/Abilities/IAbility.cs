using System;

public interface IAbility
{
    Timer Cooldown { get; }
    event Action<IAbility> OnAct;
    void Initialize(UnityEngine.Transform ownerTransform, AbilitiesManager manager, UnityEngine.Collider2D ownerCollider);
    bool CanDo();
    void Do();
}