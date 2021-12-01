using Core.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICountable
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float speed = 5.0f;

    private Transform playerTransform;

    private IInput input;
    private ITime time;
    private IAudiable memorySteal;

    public int Count
    {
        get;
        private set;
    }

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        Count = 0;
    }

    private void OnEnable()
    {
        var inputManager = GameObject.Find("InputManager");
        input = inputManager.GetComponent<IInput>();

        var timeManager = GameObject.Find("TimeManager");
        time = timeManager.GetComponent<ITime>();

        memorySteal = GetComponent<IAudiable>();
    }

    void Update()
    {
        float x = input.GetAxis("Horizontal");
        float y = input.GetAxis("Vertical");
        float xMovement = x * speed * time.DeltaTime;
        float yMovement = y * speed * time.DeltaTime;

        playerTransform.Translate(xMovement, yMovement, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Count++;
            memorySteal.Play();
            Debug.Log("Stolen memory: " + Count);
        }
    }
}
