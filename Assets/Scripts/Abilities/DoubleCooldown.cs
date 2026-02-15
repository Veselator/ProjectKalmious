using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCooldown : BaseAbility
{
    private const float _cooldownMultiplier = 2f;
    [SerializeField] private float _abilityActionTime = 2f;
    private PlayerCurrentWeapon _curWeapon;

    private Timer _abilityActionTimer = new Timer();

    public override void Initialize(Transform ownerTransform, AbilitiesManager manager, Collider2D ownerCollider, AbilitySO data)
    {
        base.Initialize(ownerTransform, manager, ownerCollider, data);
        _curWeapon = manager.PlayerCurWeapon;
    }

    private void Start()
    {
        _abilityActionTimer.OnStart += HandleAbilityStart;
        _abilityActionTimer.OnEnd += HandleAbilityEnd;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _abilityActionTimer.OnStart -= HandleAbilityStart;
        _abilityActionTimer.OnEnd -= HandleAbilityEnd;
        _abilityActionTimer.Disable();
    }

    private void HandleAbilityStart()
    {
        _curWeapon.SetOverallCooldownMultiplier(_cooldownMultiplier);
    }

    private void HandleAbilityEnd()
    {
        _curWeapon.SetOverallCooldownMultiplier(1f);
    }

    protected override void Act()
    {
        _abilityActionTimer.Start(_abilityActionTime);
    }
}
