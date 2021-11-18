using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerCollisionTests
{
    [UnitySetUp]
    public IEnumerator Setup()
    {
        var scene = SceneManager.LoadSceneAsync("Main");

        while (!scene.isDone)
        {
            yield return null;
        }

        var player = GameObject.FindGameObjectWithTag("Player");

        Assert.That(player != null);
        Assert.That(player.transform.localPosition, Is.EqualTo(Vector3.zero));
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
    public IEnumerator GivenPlayerWhenPlayerTouchesNPCThenNPCLoosesAMemory()
    {
        yield return null;
    }
}
