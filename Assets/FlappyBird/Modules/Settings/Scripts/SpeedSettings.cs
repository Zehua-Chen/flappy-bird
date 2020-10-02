using UnityEngine;

namespace FlappyBird.Settings
{
    [CreateAssetMenu(fileName = "Speed", menuName = "Settings/Speed")]
    public class SpeedSettings : ScriptableObject
    {
        [SerializeField]
        float _worldSpeed = 100.0f;

        public float WorldSpeed => _worldSpeed;
    }
}
