using Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    public float speed = 5.0f;

    private Transform playerTransform;
    private IInput input;
    private ITime time;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        float x = input.GetAxis("Horizontal");
        float xMovement = x * speed;
        xMovement *= time.DeltaTime;

        playerTransform.Translate(xMovement, 0, 0);

    }
}
