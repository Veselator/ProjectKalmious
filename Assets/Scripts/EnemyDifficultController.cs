using System;
using UnityEngine;

public class EnemyDifficultController : MonoBehaviour
{
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private EnemyDifficultData[] _difficulties;

    private void Start()
    {
        EnemyDifficultData current = _difficulties[GameSetup.Instance[PlayerChoiceType.LevelId]];

        _wavesManager.HealthSpawnModifier = current.HealthModifier;
        _wavesManager.DamageSpawnModifier = current.DamageModifier;
    }
}


// Послание будущим программистам!
// Так писать нельзя - код и данные должны быть разделены
// Но у меня сроки + отключения света, всего дают по 8 часов в день
// Так что как решение для проекта, которые не планируется развивать - это пойдёт
// Но опять же, не делайте так и не берите этот код в пример! Даже в этом проекте есть более
// удачные архитектурные решения, которые соотвествуют принципам SOLID
[Serializable]
public struct EnemyDifficultData
{
    public float HealthModifier;
    public float DamageModifier;
}