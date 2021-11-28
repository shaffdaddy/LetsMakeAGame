using Core.Interfaces;
using UnityEngine;

public class MemoryStealAudioController : MonoBehaviour, IAudiable
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    public void Play()
    {
        source.Play();
    }
}
