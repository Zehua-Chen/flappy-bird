#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.TestTools;
using UnityEditor;

public sealed class TestSceneManager : IPrebuildSetup, IPostBuildCleanup
{
    public readonly string PackagePath = Path.Combine("Packages", "com.flappybird.streaming");

    public readonly string[] ScenePaths = new string[]
    {
        Path.Combine("Tests", "Runtime", "Scenes", "SceneStreamerTests", "Root.unity"),
        Path.Combine("Tests", "Runtime", "Scenes", "SceneStreamerTests", "Fragment_0.unity"),
        Path.Combine("Tests", "Runtime", "Scenes", "SceneStreamerTests", "Fragment_1.unity"),
        Path.Combine("Tests", "Runtime", "Scenes", "SceneStreamerTests", "Fragment_2.unity"),
    };

    public void Setup()
    {
        var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        var newScenes = ScenePaths
            .Select(path => Path.Combine(PackagePath, path))
            .Select((path) => new EditorBuildSettingsScene(path, true));

        scenes.AddRange(newScenes);

        EditorBuildSettings.scenes = scenes.ToArray();
    }

    public void Cleanup()
    {
        var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        scenes.RemoveRange(scenes.Count - ScenePaths.Length, ScenePaths.Length);

        EditorBuildSettings.scenes = scenes.ToArray();
    }
}
