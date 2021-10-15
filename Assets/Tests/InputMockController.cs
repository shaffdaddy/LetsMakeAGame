using Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class InputMockController : MonoBehaviour, IInput
{
    private Dictionary<string, Mock> mock = new Dictionary<string, Mock>()
    {
        { "GetAxis", new Mock() }
    };

    public float GetAxis(string axisName)
    {
        var temp = mock["GetAxis"];
        temp.Argument = axisName;
        return temp.Value;
    }

    public Mock this[string functionName]
    {
        get => mock[functionName];
    }
}
