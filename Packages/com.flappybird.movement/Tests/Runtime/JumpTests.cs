using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using FlappyBird.Movement;

namespace FlappyBird.Movement.Tests
{
    public class JumpTests
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject obj in _gameObjects)
            {
                GameObject.Destroy(obj);
            }
        }

        [UnityTest]
        public IEnumerator Test()
        {
            var gameObject = new GameObject("Test Jump");
            _gameObjects.Add(gameObject);

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
