#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Streaming
{
    [CreateAssetMenu(menuName = "Streaming/Scene Stream", fileName = "SceneStream")]
    public sealed class SceneStream : ScriptableObject
    {
        public string[]? Scenes = default;
        public Rect ViewSize = default;

        internal Queue<SceneFragment> ActiveFragments => _activeFragments;
        internal Queue<string> ActiveFragmentScenes => _activeFragmentScenes;

        private Queue<SceneFragment> _activeFragments = new Queue<SceneFragment>();
        private Queue<string> _activeFragmentScenes = new Queue<string>();

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
                float offsetX = fragmentWithMaxX.Size.x + maxX;

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
    }
}
