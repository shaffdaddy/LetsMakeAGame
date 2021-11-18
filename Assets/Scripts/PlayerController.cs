using Interfaces;
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
        float xMovement = x * speed;
        xMovement *= time.DeltaTime;

        playerTransform.Translate(xMovement, 0, 0);

    }
}