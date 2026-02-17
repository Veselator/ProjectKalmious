using UnityEngine;

public class OnEnemySpawnedLinker : MonoBehaviour
{
    [SerializeField] private BaseAI _ai;

    private void Start()
    {
        GlobalFlags.Instance.TriggerEnemySpawned(_ai, transform.position);
    }
}
