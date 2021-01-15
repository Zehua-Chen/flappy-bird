#nullable enable

using UnityEngine;
using FlappyBird.Values;

namespace FlappyBird.Score
{
    public sealed class ScoreReceiver : MonoBehaviour
    {
        public FloatVariable? Score = default;
        public string ScoreZoneTag = "Score";

        private void Start()
        {
            if (Score != null)
            {
                Score.Value = 0.0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Score == null)
            {
                return;
            }

            if (!collision.CompareTag(ScoreZoneTag))
            {
                return;   
            }

            ScoreZone? zone = collision.gameObject.GetComponent<ScoreZone>();

            if (zone == null)
            {
                Debug.LogErrorFormat(
                    "Score zone {0} does not contain ScoreZone component",
                    collision.gameObject.name);

                return;
            }

            Score.Value += zone.Score;
        }
    }
}
