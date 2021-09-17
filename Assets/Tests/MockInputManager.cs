using Interfaces;
using UnityEngine;

public class MockInputManager : MonoBehaviour, IInput
{
    public float GetAxis(string axisName)
    {
        return 5;
    }
}
