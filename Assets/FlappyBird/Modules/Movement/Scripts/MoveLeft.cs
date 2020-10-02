using UnityEngine;
using FlappyBird.Settings;

namespace FlappyBird.Movement
{
    /// <summary>
    /// Slides a rigidbody 2d to the left; prefers Kinematic rigidbody 2d
    /// </summary>
    public class MoveLeft : MonoBehaviour
    {
        public SpeedSettings SpeedSettings = null;
        public Rigidbody2D Rigidbody2D = null;

        private void Update()
        {
            float dt = Time.deltaTime;
            float xTranslate = dt * this.SpeedSettings.WorldSpeed * -1;

            this.Rigidbody2D.velocity = new Vector2(xTranslate, 0);
        }
    }
}
