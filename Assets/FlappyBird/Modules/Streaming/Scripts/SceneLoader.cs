using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Streaming
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Current = null;

        public Queue<Fragment> Fragments = new Queue<Fragment>();

        private void Awake()
        {
            SceneLoader.Current = this;
        }

        private void OnDestroy()
        {
            SceneLoader.Current = null;
        }

        public void Add(Fragment fragment)
        {
            this.Add(fragment, new Vector3(-0.5f, 0.0f, 0.0f));
        }

        public void Add(Fragment fragment, Vector3 offset)
        {
            Vector3 position = fragment.transform.position;

            if (this.Fragments.Count > 0)
            {
                Fragment previous = this.Fragments.Peek();

                position = previous.transform.position;

                position.x += previous.Bounds.extents.x;
                position.x += fragment.Bounds.extents.x;

                position += offset;
            }

            fragment.transform.position = position;
            this.Fragments.Enqueue(fragment);
        }
    }
}
