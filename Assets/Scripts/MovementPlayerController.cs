using Core.Interfaces;
using UnityEngine;

namespace Controllers
{
    public class MovementPlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5;

        private IInput input;
        private ITime time;

        // Start is called before the first frame update
        void Start()
        {
            var inputManger = GameObject.Find("InputManager");
            input = inputManger.GetComponent<IInput>();

            var timeManager = GameObject.Find("TimeManager");
            time = timeManager.GetComponent<ITime>();
        }

        // Update is called once per frame
        void Update()
        {
            float x = input.GetAxis("Horizontal");
            float xMovement = x * speed * time.DeltaTime;

            float y = input.GetAxis("Vertical");
            float yMovement = y * speed * time.DeltaTime;

            transform.Translate(xMovement, yMovement, 0);
        }
    }
}

