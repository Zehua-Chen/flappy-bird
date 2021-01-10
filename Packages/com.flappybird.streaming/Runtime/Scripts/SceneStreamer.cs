#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public sealed class SceneStreamer : MonoBehaviour
    {
        public SceneStream? SceneStream = default;
        public int NextScene = 0;
        public int BufferSize = 2;

        private Queue<SceneFragment> _activeFragments = new Queue<SceneFragment>();
        private Queue<string> _activeFragmentScenes = new Queue<string>();

        private IEnumerator Start()
        {
            if (SceneStream != null && SceneStream.Scenes != null)
            {
                for (int i = 0; i < BufferSize && i < SceneStream.Scenes.Length; i++)
                {
                    yield return StartCoroutine(LoadNextScene());
                }
            }
        }

        private void Update()
        {
            if (RemoveFirstInvisibleFragment())
            {
                StartCoroutine(LoadNextScene());
            }
        }

        private void OnDrawGizmos()
        {
            if (SceneStream == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(SceneStream.View.position, SceneStream.View.size);
        }

        #region Scene Loading

        /// <summary>
        /// Load a scene by name and add the loaded name to <c>SceneStream.ActiveScenes</c>
        /// </summary>
        /// <param name="sceneName">the name of the scene to load</param>
        private IEnumerator LoadScene(string sceneName)
        {
            if (SceneStream == null)
            {
                yield break;
            }
            
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneByName(sceneName);

            SceneFragment? fragment = FindFragment(scene.GetRootGameObjects());

            if (fragment != null)
            {
                AddActive(fragment);
                AddActive(sceneName);
            }
            else
            {
                Debug.LogErrorFormat("Scene {0} does not have fragment", sceneName);
            }
        }

        /// <summary>
        /// Load the next scene and increments <c>NextScene</c>
        /// </summary>
        private IEnumerator LoadNextScene()
        {
            if (SceneStream == null || SceneStream.Scenes == null)
            {
                yield break;
            }

            if (NextScene >= SceneStream.Scenes.Length)
            {
                yield break;
            }

            yield return StartCoroutine(LoadScene(SceneStream.Scenes[NextScene]));

            NextScene++;
        }

        /// <summary>
        /// Add an active fragment and place the fragment after all other fragments
        /// </summary>
        /// <remarks>
        /// Intended to be called by <c>SceneStreamer</c>
        /// </remarks>
        /// <param name="fragment">an new active fragment</param>
        internal void AddActive(SceneFragment fragment)
        {
            float maxX = float.MinValue;
            SceneFragment? fragmentWithMaxX = null;

            foreach (SceneFragment existingFragment in _activeFragments)
            {
                float x = existingFragment.transform.position.x;

                if (x > maxX)
                {
                    maxX = x;
                    fragmentWithMaxX = existingFragment;
                }
            }

            if (fragmentWithMaxX != null)
            {
                float offsetX = fragmentWithMaxX.Size.x / 2.0f + fragment.Size.x / 2.0f + maxX;
                ShiftX(fragment, offsetX);
            }

            _activeFragments.Enqueue(fragment);
        }

        /// <summary>
        /// Add an active scene that contains the last added fragment.
        /// </summary>
        /// <remarks>
        /// Intended to be called by <c>SceneFragment</c>
        /// </remarks>
        /// <param name="fragmentScene">the scene of the fragment</param>
        internal void AddActive(string fragmentScene)
        {
            _activeFragmentScenes.Enqueue(fragmentScene);
        }

        /// <summary>
        /// Shift a fragment and its roots by an offset along x-axis
        /// </summary>
        /// <param name="fragment">the fragment to shift</param>
        /// <param name="offsetX">how much to shift</param>
        private void ShiftX(SceneFragment fragment, float offsetX)
        {
            var offset = new Vector3(offsetX, 0.0f);

            fragment.transform.Translate(offset);

            if (fragment.Roots != null)
            {
                for (int i = 0; i < fragment.Roots.Length; i++)
                {
                    fragment.Roots[i].Translate(offset);
                }
            }
        }

        #endregion

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

            if (_activeFragments.Count <= 0)
            {
                return false;
            }

            Debug.AssertFormat(
                _activeFragmentScenes.Count == _activeFragments.Count,
                "ActiveScenes.Count = {0}, ActiveFragments.Count = {1}",
                _activeFragmentScenes.Count,
                _activeFragments.Count);

            SceneFragment fragment = _activeFragments.Peek();
            Rect fragmentRect = fragment.Rect;
            Rect viewRect = SceneStream.View;

            //Debug.LogFormat("Peeking {0}", fragment.gameObject.scene.name);

            if (!viewRect.Overlaps(fragmentRect))
            {
                string fragmentScene = _activeFragmentScenes.Dequeue();
                _activeFragments.Dequeue();

                SceneManager.UnloadSceneAsync(fragmentScene);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Find a scene fragment in roots of a scene
        /// </summary>
        /// <remarks>
        /// Note that this method does not search beyond roots
        /// </remarks>
        /// <param name="roots">the roots of a scene</param>
        /// <returns></returns>
        private SceneFragment? FindFragment(GameObject[] roots)
        {
            for (int i = 0; i < roots.Length; i++)
            {
                var result = roots[i].GetComponent<SceneFragment>();

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
