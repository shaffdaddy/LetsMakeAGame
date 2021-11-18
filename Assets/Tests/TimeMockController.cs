using Core.Interfaces;
using UnityEngine;

public class TimeMockController : MonoBehaviour, ITime
{
    public float DeltaTime
    {
        get;
        set;
    }
}
