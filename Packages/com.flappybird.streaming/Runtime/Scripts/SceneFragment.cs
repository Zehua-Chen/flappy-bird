#nullable enable

using UnityEngine;

namespace FlappyBird.Streaming
{
    public sealed class SceneFragment : MonoBehaviour
    {
        public SceneStream? Stream = default;
        public Vector2 Size = default;
        public Transform[]? Roots = default;

        public Rect Rect
        {
            get
            {
                var position = new Vector2(transform.position.x, transform.position.y);
                return new Rect(position, Size);
            }
        }

        private void Update()
        {
            float speed = Stream != null ? Stream.Speed : 0.0f;
            Vector3 delta = Vector3.left * speed * Time.deltaTime;

            if (Roots != null)
            {
                for (int i = 0; i < Roots.Length; i++)
                {
                    Roots[i].Translate(delta);
                }
            }

            transform.Translate(delta);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Size);
        }
    }
}
