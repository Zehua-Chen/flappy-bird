using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public class Fragment : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _background = null;

        public Bounds Bounds => _background.bounds;
    }
}
