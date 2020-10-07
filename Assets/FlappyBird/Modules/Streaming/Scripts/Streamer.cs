using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public class Streamer : MonoBehaviour
    {
        public int StreamingLayer = 10;
        public string[] Scenes = null;

        private SceneLoader _sceneLoader = new SceneLoader();

        private IEnumerator Start()
        {
            yield return _sceneLoader.Enqueue("Fragment 0");
            yield return new WaitForSeconds(1.0f);

            yield return _sceneLoader.Enqueue("Fragment 1");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _sceneLoader.Dequeue();
        }
    }
}
