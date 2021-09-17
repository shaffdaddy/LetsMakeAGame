using Interfaces;
using UnityEngine;

public class MockTimeManager : MonoBehaviour, ITime
{
    public float DeltaTime => 5;
}
