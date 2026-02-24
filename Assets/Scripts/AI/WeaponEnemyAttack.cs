using UnityEngine;

public class WeaponEnemyAttack : BaseEnemyAttackAI
{
    [SerializeField] private BaseWeapon _weapon;

    protected override void Act()
    {
        _weapon.Act();
    }

    protected override bool CanAct()
    {
        return _weapon.CanAct();
    }

    public void SetOverallDamageModifier(float modifier)
    {
        _weapon.SetOverallDamageModifier(modifier);
    }
}
