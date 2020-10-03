using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Streaming
{
    public class Fragment : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _background = null;

        public Bounds Bounds => _background.bounds;

        private void Awake()
        {
            SceneLoader.Current.Add(this);
        }
    }
}
