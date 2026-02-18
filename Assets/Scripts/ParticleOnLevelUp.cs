using UnityEngine;

public class ParticleOnLevelUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem _levelUpParticleSystem;
    private GlobalFlags _globalFlags;

    private void Start()
    {
        _globalFlags = GlobalFlags.Instance;

        _globalFlags.OnLevelUp += HandleLevelUp;
    }

    private void OnDestroy()
    {
        _globalFlags.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp()
    {
        _levelUpParticleSystem.Play();
    }
}
