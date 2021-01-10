#nullable enable

using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public sealed class SceneStreamer : MonoBehaviour
    {
        public SceneStream? SceneStream = default;
        public int NextScene = 0;
        public int BufferSize = 2;

        private void Awake()
        {
            for (int i = 0; i < BufferSize; i++)
            {
                LoadNextScene();
            }
        }

        private void Update()
        {
            if (RemoveFirstInvisibleFragment())
            {
                LoadNextScene();
            }
        }

        private void OnDrawGizmos()
        {
            if (SceneStream == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(SceneStream.ViewSize.position, SceneStream.ViewSize.size);
        }

        /// <summary>
        /// Load a scene by name and add the loaded name to <c>SceneStream.ActiveScenes</c>
        /// </summary>
        /// <param name="scene">the name of the scene to load</param>
        private void LoadScene(string scene)
        {
            if (SceneStream == null)
            {
                return;
            }
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            operation.completed += (operation) =>
            {
                SceneStream.AddActive(scene);
            };
        }

        /// <summary>
        /// Remove the first invisible fragment in <c>SceneStream.ActiveFragments</c>
        /// </summary>
        /// <returns>returns true if a fragment is removed, false otherwise</returns>
        private bool RemoveFirstInvisibleFragment()
        {
            if (SceneStream == null)
            {
                return false;
            }

            if (SceneStream.ActiveFragments.Count <= 0)
            {
                return false;
            }

            Debug.AssertFormat(
                SceneStream.ActiveFragmentScenes.Count == SceneStream.ActiveFragments.Count,
                "ActiveScenes.Count = {0}, ActiveFragments.Count = {1}",
                SceneStream.ActiveFragmentScenes.Count,
                SceneStream.ActiveFragments.Count);

            SceneFragment fragment = SceneStream.ActiveFragments.Peek();
            Rect fragmentRect = fragment.Rect;
            Rect viewRect = SceneStream.ViewSize;

            if (!viewRect.Overlaps(fragmentRect))
            {
                string fragmentScene = SceneStream.ActiveFragmentScenes.Dequeue();
                SceneStream.ActiveFragments.Dequeue();

                SceneManager.UnloadSceneAsync(fragmentScene);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Load the next scene and increments <c>NextScene</c>
        /// </summary>
        private void LoadNextScene()
        {
            if (SceneStream == null || SceneStream.Scenes == null)
            {
                return;
            }

            if (NextScene >= SceneStream.Scenes.Length)
            {
                return;
            }

            LoadScene(SceneStream.Scenes[NextScene]);

            NextScene++;
        }
    }
}
