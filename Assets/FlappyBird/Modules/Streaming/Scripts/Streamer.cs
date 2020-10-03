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

        public SceneLoader SceneLoader = null;

        private IEnumerator Start()
        {
            yield return this.SceneLoader.Enqueue("Fragment 0");
            yield return new WaitForSeconds(1.0f);

            yield return this.SceneLoader.Enqueue("Fragment 1");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            this.SceneLoader.Dequeue();
        }
    }
}
