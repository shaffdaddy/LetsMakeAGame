using System.Collections;
using System.Linq;
using Core.Interfaces;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace E2E
{
    public class MainTests
    {
        private GameObject player;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            var scene = SceneManager.LoadSceneAsync("Main");

            while (!scene.isDone)
            {
                yield return null;
            }

            player = GameObject.FindGameObjectWithTag("Player");
            Assert.That(player != null, "There must be tagged player in the scene");
            Assert.That(player.transform.position, Is.EqualTo(Vector3.zero), "Player shoudl be at the center of the scene");

            var rigidbody = player.GetComponent<Rigidbody>();
            Assert.That(rigidbody != null, "Player must have a rigidbody component");
            Assert.That(rigidbody.useGravity, Is.False, "Player must not be using gravity");

            var collider = player.GetComponent<Collider>();
            Assert.That(collider != null, "Player must have a collider");
            Assert.That(collider.isTrigger, Is.True, "Player must have a triggerable collider");

            var audio = player.GetComponent<AudioSource>();
            Assert.That(audio != null, "The player must have audio mock");
            Assert.That(audio.isPlaying, Is.False, "Audio should not be playing");

            var inputManager = GameObject.Find("InputManager");
            Assert.That(inputManager != null, "There must be an input manager");

            var inputController = inputManager.GetComponent<IInput>();
            Assert.That(inputController != null, "The input manager must implement IInput");

            var timeManager = GameObject.Find("TimeManager");
            Assert.That(timeManager != null, "There must be a time manager");

            var timeController = timeManager.GetComponent<ITime>();
            Assert.That(timeController != null, "Time manager must implement ITime");

            var controllers = player.GetComponents<MonoBehaviour>();
            var names = controllers.Select(c => c.GetType().Name);

            Assert.That(names.Contains("MovementPlayerController"), Is.True, "Player should have MovementPlayerController");

            var countController = player.GetComponent<ICountable>();
            Assert.That(countController != null, "Player should implement ICountable");

            var audioController = player.GetComponent<IAudiable>();
            Assert.That(audioController != null, "Player should implement IAudiable");

            var score = GameObject.Find("Score");
            Assert.That(score != null, "There should be a score object");

            var scoreDisplay = score.GetComponent<TextMeshProUGUI>();
            Assert.That(scoreDisplay != null, "There should be a text gui object");
            Assert.That(scoreDisplay.text, Is.EqualTo("0"));

            var npcs = GameObject.FindGameObjectsWithTag("NPC");
            Assert.That(npcs.All(npc => npc.GetComponent<IParticleSystem>() != null), Is.True, "All NPCs should implement IParticleSystem");
            Assert.That(npcs.All(npc => npc.GetComponentInChildren<ParticleSystem>() != null), "All NPCs should have a particle system");
            var particles = npcs.Select(npc => npc.GetComponentInChildren<ParticleSystem>());
            Assert.That(particles.All(p => !p.isPlaying), Is.True, "No NPC particles should be playing");

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
        public IEnumerator GivenPlayerWhenPlayerCollidesWithNPCThenCountIsIncreasedByOne()
        {
            var npc = GameObject.FindGameObjectWithTag("NPC");
            player.transform.position = npc.transform.position;

            yield return null;

            var countController = player.GetComponent<ICountable>();

            Assert.That(countController.Count, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenPlayerCollidesWithNPCThenAudioIsPlayed()
        {
            var npc = GameObject.FindGameObjectWithTag("NPC");
            player.transform.position = npc.transform.position;

            yield return null;

            var audio = player.GetComponent<AudioSource>();

            Assert.That(audio.isPlaying, Is.True);
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenPlayerCollidesWithNPCThenNPCParticleIsPlayed()
        {
            var npc = GameObject.FindGameObjectWithTag("NPC");
            player.transform.position = npc.transform.position;

            yield return null;

            var particle = npc.GetComponentInChildren<ParticleSystem>();

            Assert.That(particle.isPlaying, Is.True);
        }

        [UnityTest]
        public IEnumerator GivenPlayerWhenPlayerCollidesWithNPCThenScoreDisplayIsOne()
        {
            var npc = GameObject.FindGameObjectWithTag("NPC");
            player.transform.position = npc.transform.position;

            yield return null;

            var display = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

            yield return null;

            Assert.That(display.text, Is.EqualTo("1"));
        }
    }
}
