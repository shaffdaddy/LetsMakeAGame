using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

public class ParticleMockController : MonoBehaviour, IParticleSystem
{
    private readonly Dictionary<string, Mock> mock = new Dictionary<string, Mock>()
    {
        { "Play", new Mock() }
    };

    public void Play()
    {
        var tmp = mock["Play"];
        tmp.Count++;
    }

    public Mock this[string functionName]
    {
        get => mock[functionName];
    }
}
