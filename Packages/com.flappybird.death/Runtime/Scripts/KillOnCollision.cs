#nullable enable

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyBird.Death
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class KillOnCollision : MonoBehaviour
    {
        public string Tag = "World";
        public float JumpForce = 2.0f;
        public float WaitBeforeDestroy = 1.5f;

        public Rigidbody2D? Rigidbody2D = default;
        public Collider2D? Collider2D = default;

        /// <summary>
        /// Invoked upon colliding with the world. But before self destruction
        /// </summary>
        public UnityEvent CollidedWithWorld = new UnityEvent();

        /// <summary>
        /// Invoked after self destruction
        /// </summary>
        public UnityEvent Killed = new UnityEvent();

        private void OnCollisionEnter2D(Collision2D collision)
        { 
            if (Rigidbody2D == null)
            {
                return;
            }

            if (Collider2D == null)
            {
                return;
            }

            if (collision.gameObject.CompareTag(Tag))
            {
                var force = new Vector2(0.0f, JumpForce);
                Rigidbody2D.velocity = force;

                CollidedWithWorld.Invoke();
                StartCoroutine(SelfDestroy());
            }
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(WaitBeforeDestroy);
            Destroy(gameObject);

            Killed.Invoke();
        }
    }
}
