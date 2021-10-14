using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    [UnitySetUp]
    public IEnumerator Setup()
    {
        var scene = SceneManager.LoadSceneAsync("Main");

        while(!scene.isDone)
        {
            yield return null;
        }

        var player = GameObject.FindGameObjectWithTag("Player");

        Assert.That(player != null);
        Assert.That(player.transform.localPosition, Is.EqualTo(Vector3.zero));

        player.SetActive(false);

        yield return null;

        var inputManager = GameObject.Find("InputManager");
        Object.Destroy(inputManager);

        var mockInputManager = new GameObject("InputManager");
        mockInputManager.AddComponent<MockInputManager>();

        var timeManager = GameObject.Find("TimeManager");
        Object.Destroy(timeManager);

        var mockTimeManager = new GameObject("TimeManager");
        mockTimeManager.AddComponent<MockTimeManager>();

        yield return null;

        player.SetActive(true);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        var scene = SceneManager.GetActiveScene();
        var objects = scene.GetRootGameObjects();

        foreach(var obj in objects)
        {
            obj.SetActive(false);
            Object.Destroy(obj);

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator GivenUserWhenUserEntersInHorizontalInputThenPlayableCharacterIsMovedAlongXAxis()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        
        yield return null;

        Assert.That(player.transform.position.x, Is.EqualTo(500));
    }
}
