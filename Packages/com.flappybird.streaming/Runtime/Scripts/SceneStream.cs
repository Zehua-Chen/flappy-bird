#nullable enable

using UnityEngine;

namespace FlappyBird.Streaming
{
    [CreateAssetMenu(menuName = "Streaming/Scene Stream", fileName = "SceneStream")]
    public sealed class SceneStream : ScriptableObject
    {
        public string[]? Scenes = default;
        public Rect View = default;
        public float Speed = 1.5f;
    }
}
