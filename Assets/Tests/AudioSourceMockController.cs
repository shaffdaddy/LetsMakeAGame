using System.Collections;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

public class AudioSourceMockController : MonoBehaviour, IAudiable
{
    private readonly Dictionary<string, Mock> mock = new Dictionary<string, Mock>()
    {
        { "Play", new Mock() }
    };

    public void Play()
    {
        var temp = mock["Play"];
        temp.Count++;
    }

    public Mock this[string functionName]
    {
        get => mock[functionName];
    }
}
