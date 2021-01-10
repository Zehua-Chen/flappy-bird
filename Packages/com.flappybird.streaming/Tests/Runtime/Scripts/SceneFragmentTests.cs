#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using FlappyBird.Streaming;

public sealed class SceneFragmentTests
{
    List<GameObject> _gameObjects = new List<GameObject>();

    [TearDown]
    public void TearDown()
    {
        foreach (var obj in _gameObjects)
        {
            GameObject.Destroy(obj);
        }
    }

    [UnityTest]
    public IEnumerator MoveRoots()
    {
        var fragmentGameObject = new GameObject("Fragment");
        _gameObjects.Add(fragmentGameObject);

        var stream = ScriptableObject.CreateInstance<SceneStream>();
        var fragment = fragmentGameObject.AddComponent<SceneFragment>();

        var testA = new GameObject("Test A");
        _gameObjects.Add(testA);

        var testB = new GameObject("Test B");
        _gameObjects.Add(testB);

        fragment.Stream = stream;
        fragment.Roots = new Transform[] { testA.transform, testB.transform };

        yield return new WaitForSeconds(2.0f);

        Assert.That(testA.transform.position.x, Is.Negative);
        Assert.That(testB.transform.position.x, Is.Negative);
    }
}
