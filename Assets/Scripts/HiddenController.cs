using Core.Interfaces;
using UnityEngine;

public class HiddenController : MonoBehaviour, IAudiable
{
    private AudioSource soundEffect;

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }


    public void Play()
    {
        soundEffect.Play();
    }
}
