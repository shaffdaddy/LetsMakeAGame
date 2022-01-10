using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Controllers
{
    public class MovementPlayerControllerTests
    {
        private InputMockController input;
        private TimeMockController time;

        private GameObject sut;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            var scene = SceneManager.LoadSceneAsync("MovementPlayerController");

            while (!scene.isDone)
            {
                yield return null;
            }

            sut = GameObject.FindGameObjectWithTag("Player");
            Assert.That(sut != null, "There must be tagged player in the scene");
            Assert.That(sut.transform.position, Is.EqualTo(Vector3.zero), "Player shoudl be at the center of the scene");

            var inputManger = GameObject.Find("InputManager");
            Assert.That(inputManger != null, "There must be an input manager available");

            input = inputManger.GetComponent<InputMockController>();
            Assert.That(input != null, "There must be an input mock available");

            var timeManager = GameObject.Find("TimeManager");
            Assert.That(timeManager != null, "There must be a time manager available");

            time = timeManager.GetComponent<TimeMockController>();
            Assert.That(time != null, "There must be a time mock available");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            var scene = SceneManager.GetActiveScene();
            var objects = scene.GetRootGameObjects();

            foreach (var obj in objects)
            {
                obj.SetActive(false);
                Object.Destroy(obj);

                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        [UnityTest]
        public IEnumerator GivenInputWhenUserInputsHorizontalThenPlayerIsMovedAlongTheXAxis()
        {
            input["GetAxis"].CalledWith("Horizontal").Returns(2);

            time.DeltaTime = 1;

            yield return null;

            Assert.That(sut.transform.position.x, Is.EqualTo(10));
        }

        [UnityTest]
        public IEnumerator GivenInputWhenUserInputsHorizontalThenNoMovementAlongZAxis()
        {
            input["GetAxis"].CalledWith("Horizontal").Returns(2);

            time.DeltaTime = 1;

            yield return null;

            Assert.That(sut.transform.position.z, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator GivenInputWhenUserInputsVerticalThenPlayerIsMovedAlongTheYAxis()
        {
            input["GetAxis"].CalledWith("Vertical").Returns(2);

            time.DeltaTime = 1;

            yield return null;

            Assert.That(sut.transform.position.y, Is.EqualTo(10));
        }

        [UnityTest]
        public IEnumerator GivenInputWhenUserInputsVerticalThenNoMovementAlongZAxis()
        {
            input["GetAxis"].CalledWith("Vertical").Returns(2);

            time.DeltaTime = 1;

            yield return null;

            Assert.That(sut.transform.position.z, Is.EqualTo(0));
        }
    }
}

