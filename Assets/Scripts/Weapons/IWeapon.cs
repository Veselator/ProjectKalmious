using UnityEngine;
using System;

public interface IWeapon
{
    Damage DealedDamage { get; }
    void Act();
    bool CanAct();
}