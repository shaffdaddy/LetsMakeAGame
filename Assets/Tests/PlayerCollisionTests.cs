using System.Collections;
using Core.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerCollisionTests
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

        Assert.That(player != null);
        Assert.That(player.transform.localPosition, Is.EqualTo(Vector3.zero));

        var counter = player.GetComponent<ICountable>();
        Assert.That(counter != null);
        Assert.That(counter.Count, Is.EqualTo(0));

        player.SetActive(false);

        var audio = player.GetComponent<IAudiable>();
        Object.Destroy(audio as MonoBehaviour);

        yield return null;

        var audioMock = player.AddComponent<AudioSourceMockController>();
        Assert.That(audioMock != null);

        player.SetActive(true);

        yield return null;
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
    public IEnumerator GivenPlayerWhenPlayerTouchesAGameObjectThenPlayerIsNotAwardedAMemory()
    {
        var gameObject = new GameObject();

        yield return null;

        player.transform.position = gameObject.transform.position;

        yield return null;

        var counter = player.GetComponent<ICountable>();

        Assert.That(counter.Count, Is.EqualTo(0));
    }

    [UnityTest]
    public IEnumerator GivenPlayerWhenPlayerTouchesNPCThenPlayerIsAwardedMemory()
    {
        var npc = GameObject.FindGameObjectWithTag("NPC");
        player.transform.position = npc.transform.position;

        yield return null;

        var counter = player.GetComponent<ICountable>();

        Assert.That(counter.Count, Is.EqualTo(1));
    }

    [UnityTest]
    public IEnumerator GivenPlayerWhenPlayerTouchesAGameObjectThenMemoryStealAudioIsNotPlayed()
    {
        var gameObject = new GameObject();
        player.transform.position = gameObject.transform.position;

        yield return null;

        var audio = player.GetComponent<AudioSourceMockController>();

        Assert.That(audio["Play"].IsNotCalled(), Is.True);
    }

    [UnityTest]
    public IEnumerator GivenPlayerWhenPlayerTouchesNPCThenMemoryStealAudioIsPlayed()
    {
        var npc = GameObject.FindGameObjectWithTag("NPC");
        player.transform.position = npc.transform.position;

        yield return null;

        var audio = player.GetComponent<AudioSourceMockController>();

        Assert.That(audio["Play"].CalledOnce(), Is.True);
    }
}
