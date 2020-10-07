using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    /// <summary>
    /// Scene Loader load scenes and positions them correctly
    /// </summary>
    internal class SceneLoader
    {
        private List<Fragment> _fragments = new List<Fragment>();

        /// <summary>
        /// Remove the oldest scene
        /// </summary>
        public void Dequeue()
        {
            if (_fragments.Count > 0)
            {
                Fragment fragment = _fragments[0];
                SceneManager.UnloadSceneAsync(fragment.gameObject.scene.buildIndex);

                _fragments.RemoveAt(0);
            }
        }

        /// <summary>
        /// Load a new scene; if the loader has not loaded any scenes, the roots
        /// of the old scene would not be offseted; otherwise, the new scene
        /// would be placed following the previous scene with offset in between
        /// </summary>
        /// <param name="sceneName">name of the new scene</param>
        /// <returns>An async operation</returns>
        public IEnumerator Enqueue(string sceneName)
        {
            return this.Enqueue(sceneName, new Vector3(-0.5f, 0.0f, 0.0f));
        }

        /// <summary>
        /// Load a new scene; if the loader has not loaded any scenes, the roots
        /// of the old scene would not be offseted; otherwise, the new scene
        /// would be placed following the previous scene with offset in between
        /// </summary>
        /// <param name="sceneName">name of the new scene</param>
        /// <param name="offset">offet to applied to the new scene</param>
        /// <returns></returns>
        public IEnumerator Enqueue(string sceneName, Vector3 offset)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Additive
            });

            yield return operation;

            Scene scene = SceneManager.GetSceneByName(sceneName);
            GameObject[] roots = scene.GetRootGameObjects();

            this.Enqueue(roots, offset);
        }

        /// <summary>
        /// Load a new scene; if the loader has not loaded any scenes, the roots
        /// of the old scene would not be offseted; otherwise, the new scene
        /// would be placed following the previous scene with offset in between
        /// </summary>
        /// <param name="roots">roots of the new scene</param>
        /// <param name="offset">offset applied to the new scene</param>
        public void Enqueue(GameObject[] roots, Vector3 offset)
        {
            Fragment fragment = null;

            for (int i = 0; i < roots.Length; i++)
            {
                var found = roots[i].GetComponent<Fragment>();

                if (found != null)
                {
                    fragment = found;
                }
            }

            if (fragment == null)
            {
                Debug.LogError("Fragment component not found in roots");
                return;
            }

            if (_fragments.Count > 0)
            {
                Fragment previous = _fragments[_fragments.Count - 1];
                Vector3 previousPosition = previous.transform.position;

                for (int i = 0; i < roots.Length; i++)
                {
                    Vector3 position = roots[i].transform.position;

                    position.x = previousPosition.x;
                    position.x += previous.Bounds.extents.x;
                    position.x += fragment.Bounds.extents.x;

                    position += offset;

                    roots[i].transform.position = position; 
                }
            }

            _fragments.Add(fragment);
        }
    }
}
