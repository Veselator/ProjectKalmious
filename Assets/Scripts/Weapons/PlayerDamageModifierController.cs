using UnityEngine;

public class PlayerDamageModifierController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _currentWeapon;

    public void SetDamageModifier(float damageModifier)
    {
        _currentWeapon.SetOverallDamageModifier(damageModifier);
    }
}