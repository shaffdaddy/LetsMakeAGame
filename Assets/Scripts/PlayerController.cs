using Core.Interfaces;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICountable
{
    [SerializeField]
    private ParticleSystem memoryParticle;

    [SerializeField]
    private TextMeshProUGUI score;


    private IAudiable memorySteal;

    public int Count
    {
        get;
        private set;
    }

    void Start()
    {
        Count = 0;
    }

    private void OnEnable()
    {
        memorySteal = GetComponent<IAudiable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Count++;
            memorySteal.Play();
            memoryParticle.Play();
            Debug.Log("Stolen memory: " + Count);
            score.text = Count.ToString();
        }
    }
}
