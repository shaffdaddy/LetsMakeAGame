using Core.Interfaces;
using UnityEngine;

public class NPCParticleSystemController : MonoBehaviour, IParticleSystem
{
    [SerializeField]
    private ParticleSystem particleEffect;

    public void Play()
    {
        particleEffect.Play();
    }
}
