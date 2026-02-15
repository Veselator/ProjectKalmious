using UnityEngine;

public class PlayParticleOnAbilityAct : MonoBehaviour
{
    [SerializeField] private BaseAbility _ability;
    [SerializeField] private ParticleSystem[] _particleSystem;

    private void Start()
    {
        _ability.OnAct += PlayParticle;
    }

    private void OnDestroy()
    {
        _ability.OnAct -= PlayParticle;
    }

    private void PlayParticle(IAbility _)
    {
        foreach (var particle in _particleSystem)
        {
            particle.Play();
        }
    }
}
