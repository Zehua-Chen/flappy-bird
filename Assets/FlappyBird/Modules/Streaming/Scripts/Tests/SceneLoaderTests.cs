using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace FlappyBird.Streaming.Tests
{
    [TestFixture]
    public class SceneLoaderTests
    {
        private Sprite _sprite = null;

        [SetUp]
        public void Setup()
        {
            _sprite = Sprite.Create(
                new Texture2D(50, 50),
                new Rect(new Vector2(), new Vector2(50, 50)),
                new Vector2(),
                1);
        }

        [Test]
        public void TestEmpty()
        {
            var loader = new SceneLoader();
            GameObject[] roots = this.GetScene();

            loader.Enqueue(roots, new Vector3());

            foreach (GameObject root in roots)
            {
                Assert.AreEqual(new Vector3(), root.transform.position);
            }
        }

        [Test]
        public void TestNonEmtpy()
        {
            GameObject[] sceneA = this.GetScene();
            GameObject[] sceneB = this.GetScene();

            var loader = new SceneLoader();

            loader.Enqueue(sceneA, new Vector3());
            loader.Enqueue(sceneB, new Vector3());

            foreach (GameObject root in sceneA)
            {
                Assert.AreEqual(new Vector3(), root.transform.position);
            }

            foreach (GameObject root in sceneB)
            {
                Assert.AreEqual(new Vector3(50, 0), root.transform.position);
            }
        }

        private GameObject[] GetScene()
        {
            var roots = new GameObject[2];

            roots[0] = new GameObject();
            roots[0].transform.position = new Vector3();

            roots[1] = new GameObject();
            var renderer = roots[1].AddComponent<SpriteRenderer>();
            renderer.sprite = _sprite;

            var fragment = roots[1].AddComponent<Fragment>();
            fragment.Background = renderer;

            roots[1].transform.position = new Vector3();

            return roots;
        }
    }
}
