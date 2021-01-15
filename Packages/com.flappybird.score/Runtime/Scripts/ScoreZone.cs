#nullable enable

using UnityEngine;

namespace FlappyBird.Score
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ScoreZone : MonoBehaviour
    {
        public float Score = 1.0f;
    }
}
