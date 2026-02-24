using System;
using UnityEngine;

public interface IHealth
{
    float MaximumHealth { get; set; }
    float CurrentHealth { get; set; } // Может, сокрыть для сеттера?

    // Если есть броня, то урон сниженый
    float MaximumArmor { get; set; } // Сколько максимум может быть брони
    float CurrentArmor { get; set; } // Какой текущий показатель брони
    // Параметр урона по броне перенесён в struct Damage для большей гибкости
    //float ArmorFactor { get; set; } // Какой процент от изначального урона пройдёт по броне

    bool DoesHaveArmor { get; }
    bool IsDied { get; }
    bool IsDamaged { get; }
    float CurrentHealthInPercentage { get; }
    float CurrentArmorInPercentage { get; }
    Action<float, Collider2D> OnDamaged { get; set; }
    Action<float> OnHealthChanged { get; set; }
    Action OnArmorDestoyed { get; set; }
    Action<float> OnArmorChanged { get; set; }
    Action OnDeath { get; set;  }
    abstract void Reset();
    abstract void TakeDamage(Damage damage, Collider2D source); // заменить damage на struct damage, для большей модификации урона
    // Ps OnCollisionEnter2D обрабатывает пуля
}
