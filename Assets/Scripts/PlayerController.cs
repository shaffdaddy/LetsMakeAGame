using Core.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public float speed = 5.0f;

    private Transform playerTransform;
    private IInput input;
    private ITime time;

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        var inputManager = GameObject.Find("InputManager");
        input = inputManager.GetComponent<IInput>();

        var timeManager = GameObject.Find("TimeManager");
        time = timeManager.GetComponent<ITime>();
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
        if(other.CompareTag("NPC"))
        {
            Debug.Log("Steal memory");
        }
    }
}
