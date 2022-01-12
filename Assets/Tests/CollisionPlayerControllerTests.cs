using System.Collections;
using Core.Interfaces;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Controllers
{
    public class CollisionPlayerControllerTests
    {
        private ICountable counter;
        private AudioSourceMockController audio;
        private TextMeshProUGUI display;

        private GameObject sut;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            var scene = SceneManager.LoadSceneAsync("CollisionPlayerController");

            while (!scene.isDone)
            {
                yield return null;
            }

            sut = GameObject.FindGameObjectWithTag("Player");
            Assert.That(sut != null, "There must be tagged player in the scene");
            Assert.That(sut.transform.position, Is.EqualTo(Vector3.zero), "Player shoudl be at the center of the scene");

            var rigidbody = sut.GetComponent<Rigidbody>();
            Assert.That(rigidbody != null, "Player must have a rigidbody component");
            Assert.That(rigidbody.useGravity, Is.False, "Player must not be using gravity");

            var collider = sut.GetComponent<Collider>();
            Assert.That(collider != null, "Player must have a collider");
            Assert.That(collider.isTrigger, Is.True, "Player must have a triggerable collider");

            audio = sut.GetComponent<AudioSourceMockController>();
            Assert.That(audio != null, "The player must have audio mock");

            var text = GameObject.Find("DisplayManager");
            Assert.That(text != null, "There should be a display available");

            display = text.GetComponent<TextMeshProUGUI>();
            Assert.That(display != null, "There must be text in the display");
            Assert.That(display.text, Is.EqualTo("0"), "Display should be zero");

            counter = sut.GetComponent<ICountable>();
            Assert.That(counter != null, "The player must be countable");
            Assert.That(counter.Count, Is.EqualTo(0), "Counter must start at zero");
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
        public IEnumerator GivenPlayerWhenCollisionIsNotNPCThenCountIsNotIncreased()
        {
            var go = new GameObject("NotAnNPC");
            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(counter.Count, Is.EqualTo(0), "Should not increase counter");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsAnNPCThenCountIsIncreaesdByOne()
        {
            var go = new GameObject("SomeNPC")
            {
                tag = "NPC"
            };

            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(counter.Count, Is.EqualTo(1), "Should have increased counter");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsAnNPCThenAudioIsPlayed()
        {
            var go = new GameObject("SomeNPC")
            {
                tag = "NPC"
            };
            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(audio["Play"].CalledOnce(), Is.True, "Should have played audio");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsNotNPCThenNoAudioIsPlayed()
        {
            var go = new GameObject("NotAnNPC");
            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(audio["Play"].IsNotCalled(), Is.True, "Should not play audio");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsAnNPCThenNPCParticleIsPlayed()
        {
            var go = new GameObject("SomeNPC")
            {
                tag = "NPC"
            };
            _ = go.AddComponent<CapsuleCollider>();
            var particle = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(particle["Play"].CalledOnce(), Is.True, "Should have played particle effect");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsNotAnNPCThenNPCParticleIsNotPlayed()
        {
            var go = new GameObject("SomeNPC");
            _ = go.AddComponent<CapsuleCollider>();
            var particle = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(particle["Play"].IsNotCalled(), Is.True, "Should have not played particle effect");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsAnNPCThenScoreIncreaseIsDisplayed()
        {
            var go = new GameObject("SomeNPC")
            {
                tag = "NPC"
            };
            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(display.text, Is.EqualTo("1"), "Score should be displaying one");
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenCollisionIsNotNPCThenDisplayIsZero()
        {
            var go = new GameObject("NotAnNPC");
            _ = go.AddComponent<CapsuleCollider>();
            _ = go.AddComponent<ParticleMockController>();

            yield return null;

            sut.transform.position = go.transform.position;

            yield return null;

            Assert.That(display.text, Is.EqualTo("0"), "Display should be zero");
        }
    }
}
