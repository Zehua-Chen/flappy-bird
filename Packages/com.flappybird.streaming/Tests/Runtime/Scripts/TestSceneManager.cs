#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.TestTools;
using UnityEditor;

public sealed class TestSceneManager : IPrebuildSetup, IPostBuildCleanup
{
    public const string PackagePath = "Packages/com.flappybird.streaming";

    public string[] ScenePaths = new string[]
    {
        "Tests/Runtime/Scenes/SceneStreamerTests/Root.unity",
        "Tests/Runtime/Scenes/SceneStreamerTests/Fragment_0.unity",
        "Tests/Runtime/Scenes/SceneStreamerTests/Fragment_1.unity",
        "Tests/Runtime/Scenes/SceneStreamerTests/Fragment_2.unity"
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
