using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace FlappyBird.Streaming.Tests
{
    public class StreamerTests
    {
        [UnityTest]
        public void Test()
        {
            var streamerObject = new GameObject();
            streamerObject.transform.position = new Vector3();

            var sceneLoader = streamerObject.AddComponent<SceneLoader>();
            var streamer = streamerObject.AddComponent<Streamer>();

            streamer.SceneLoader = sceneLoader;
        }
    }
}
