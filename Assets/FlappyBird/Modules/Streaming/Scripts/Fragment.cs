using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public class Fragment : MonoBehaviour
    {
        internal SpriteRenderer Background = null;
        public Bounds Bounds => this.Background.bounds;
    }
}
