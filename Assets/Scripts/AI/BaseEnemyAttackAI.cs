using UnityEngine;

public abstract class BaseEnemyAttackAI : MonoBehaviour
{
    // Базовый класс ИИ для атаки
    // Забавно получается - ИИ на самом деле имеет два мозга. Раздвоение личности?

    [SerializeField] protected BaseAI _ai;
    [SerializeField] private float _attackDistance = 0.5f;

    private void Start()
    {
        if(_ai == null) _ai = GetComponent<BaseAI>();
    }

    private void Update()
    {
        if (CheckDistance() && CanAct()) Act();
    }

    private bool CheckDistance()
    {
        return _ai.DistanceToPlayer < _attackDistance;
    }

    protected abstract bool CanAct();
    protected abstract void Act();
}
