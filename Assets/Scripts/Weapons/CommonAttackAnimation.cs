using System.Collections;
using UnityEngine;

public class CommonAttackAnimation : BaseWeaponAttackAnimation
{
    [SerializeField] protected Animator _linkedAnimator;
    [SerializeField] protected string _animationString = "Shoot";
    private int _animationHash;

    protected override IEnumerator AttackAnimation()
    {
        _linkedAnimator.SetTrigger(_animationHash);
        yield break;
    }

    protected override void Init()
    {
        _animationHash = Animator.StringToHash(_animationString);
    }
}
