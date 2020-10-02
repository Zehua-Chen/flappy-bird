using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using FlappyBird.Settings;

namespace FlappyBird.Movement.Tests
{
    public class JumpTests
    {
        [UnityTest]
        public IEnumerator Test()
        {
            var gameObject = new GameObject("Test Jump");
            gameObject.transform.position = new Vector3();

            var rig = gameObject.AddComponent<Rigidbody2D>();
            var jump = gameObject.AddComponent<Jump>();

            jump.Force = 50.0f;
            jump.Rigidbody2D = rig;

            jump.Perform();

            yield return new WaitForSeconds(0.5f);

            Assert.Greater(gameObject.transform.position.y, 0.0f);
        }
    }
}
