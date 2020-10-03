using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Streaming
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Current = null;

        public Queue<Fragment> Fragments = new Queue<Fragment>();

        private void Awake()
        {
            SceneLoader.Current = this;
        }

        private void OnDestroy()
        {
            SceneLoader.Current = null;
        }

        public void Dequeue()
        {
            if (this.Fragments.Count > 0)
            {
                Fragment fragment = this.Fragments.Dequeue();
                SceneManager.UnloadSceneAsync(fragment.gameObject.scene.buildIndex);
            }
        }

        public IEnumerator Enqueue(string sceneName)
        {
            return this.Enqueue(sceneName, new Vector3(-0.5f, 0.0f, 0.0f));
        }

        public IEnumerator Enqueue(string sceneName, Vector3 offset)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Additive
            });

            yield return operation;

            Scene scene = SceneManager.GetSceneByName(sceneName);
            GameObject[] roots = scene.GetRootGameObjects();

            Fragment fragment = null;

            for (int i = 0; i < roots.Length; i++)
            {
                fragment = roots[i].GetComponent<Fragment>();

                if (fragment != null)
                {
                    break;
                }
            }

            if (fragment == null)
            {
                Debug.LogErrorFormat("{0} does not contain Fragment in root", sceneName);
            }

            Vector3 position = fragment.transform.position;

            if (this.Fragments.Count > 0)
            {
                Fragment previous = this.Fragments.Peek();

                position = previous.transform.position;

                position.x += previous.Bounds.extents.x;
                position.x += fragment.Bounds.extents.x;

                position += offset;
            }

            fragment.transform.position = position;
            this.Fragments.Enqueue(fragment);
        }
    }
}
