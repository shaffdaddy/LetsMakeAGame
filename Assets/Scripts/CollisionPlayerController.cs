using Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
    private async void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NPC"))
        {
            Count++;
            soundEffect.Play();
            var particle = other.GetComponent<IParticleSystem>();
            particle.Play();
            
            score.text = Count.ToString();

            if(GameObject.FindGameObjectsWithTag("Puzzle").Length == 0)
            {
                var handle = Addressables.LoadAssetAsync<GameObject>("Puzzle");
                var puzzle = await handle.Task;
                _ = Instantiate(puzzle);

                var player = GameObject.FindGameObjectWithTag("Player");
                player.SetActive(false);
                var npcs = GameObject.FindGameObjectsWithTag("NPC");
                foreach(var npc in npcs)
                {
                    npc.SetActive(false);
                }
            }
        }
    }

}
