using System.Collections;
using UnityEngine;

public abstract class BaseWeaponAttackAnimation : MonoBehaviour
{
    [SerializeField] protected float _animationDuration = 0.3f;
    protected BaseWeapon _linkedWeapon;

    private void Start()
    {
        _linkedWeapon = GetComponent<BaseWeapon>();
        _linkedWeapon.OnActStarted += StartAnimation;

        Init();
    }

    protected abstract void Init();

    private void OnDestroy()
    {
        _linkedWeapon.OnActStarted -= StartAnimation;
    }

    private void StartAnimation()
    {
        StartCoroutine(AttackAnimation());
    }

    protected abstract IEnumerator AttackAnimation();
}
