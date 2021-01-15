#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;

[PrebuildSetup(typeof(TestSceneManager))]
[PostBuildCleanup(typeof(TestSceneManager))]
public sealed class SceneStreamerTests
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return SceneManager.LoadSceneAsync("Root", LoadSceneMode.Additive);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        for (int i = 1; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            yield return SceneManager.UnloadSceneAsync(scene.name);
        }   
    }

    /// <summary>
    /// Make sure that we have enough scenes in the beginning
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator InitialScenes()
    {
        yield return new WaitForSeconds(0.5f);

        string[] sceneNames = new string[SceneManager.sceneCount];

        for (int i = 0; i < sceneNames.Length; i++)
        {
            sceneNames[i] = SceneManager.GetSceneAt(i).name;
        }

        Assert.That(sceneNames.Contains("Fragment_0"));
        Assert.That(sceneNames.Contains("Fragment_1"));
    }

    /// <summary>
    /// After enough time, some scenes should have been removed as they are not out of visible
    /// rect
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator RemoveInvisibleScenes()
    {
        var unloadedScenes = new List<string>();

        SceneManager.sceneUnloaded += (scene) => { unloadedScenes.Add(scene.name); };

        yield return new WaitForSeconds(5.0f);

        Assert.That(unloadedScenes.Count, Is.GreaterThan(0));
    }
}
