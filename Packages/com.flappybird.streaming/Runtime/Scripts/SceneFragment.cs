#nullable enable

using UnityEngine;

namespace FlappyBird.Streaming
{
    public sealed class SceneFragment : MonoBehaviour
    {
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

        public float Speed = 4.0f;

        private void Update()
        {
            Vector3 delta = Vector3.left * Speed * Time.deltaTime;

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
