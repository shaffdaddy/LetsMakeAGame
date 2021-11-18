using Core.Interfaces;
using UnityEngine;

public class UnityTimeManager : MonoBehaviour, ITime
{
    public float DeltaTime => Time.deltaTime;
}
