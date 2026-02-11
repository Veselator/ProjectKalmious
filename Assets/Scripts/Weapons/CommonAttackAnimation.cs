using System.Collections;
using UnityEngine;

public class CommonAttackAnimation : BaseWeaponAttackAnimation
{
    [SerializeField] private Animator _linkedAnimator;
    [SerializeField] private string _animationString;
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
