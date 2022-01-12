using Core.Interfaces;
using TMPro;
using UnityEngine;

public class CollisionPlayerController : MonoBehaviour, ICountable
{
    [SerializeField]
    private TextMeshProUGUI score;

    private IAudiable soundEffect;

    public int Count
    {
        get;
        private set;
    }

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        soundEffect = GetComponent<IAudiable>();
        score.text = "0";
    }


    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NPC"))
        {
            Count++;
            soundEffect.Play();
            var particle = other.GetComponent<IParticleSystem>();
            particle.Play();
            score.text = Count.ToString();
        }
    }

}
