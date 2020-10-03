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
            SceneManager.LoadScene("Fragment 0", LoadSceneMode.Additive);
            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene("Fragment 1", LoadSceneMode.Additive);
        }
    }
}
