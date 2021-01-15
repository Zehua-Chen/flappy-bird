using UnityEngine;

namespace FlappyBird.Movement
{
    public sealed class Jump : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D = null;
        public float Force = 2.0f;

        public void Perform()
        {
            var force = new Vector2(0.0f, this.Force);
            Rigidbody2D.velocity = force;
        }
    }
}
