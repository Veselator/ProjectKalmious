using System;

public interface IAbility
{
    Timer Cooldown { get; }
    AbilitySO AbilityData { get; }
    event Action<IAbility> OnAct;
    void Initialize(UnityEngine.Transform ownerTransform, AbilitiesManager manager, UnityEngine.Collider2D ownerCollider, AbilitySO data);
    bool CanDo();
    void Do();
}