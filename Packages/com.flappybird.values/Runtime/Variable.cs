using UnityEngine;

namespace FlappyBird.Values
{
    public class Variable<T> : ScriptableObject
    {
        public T InitialValue { get; set; }
        public T Value { get; set; }

        private void OnEnable()
        {
            Value = InitialValue;
        }
    }
}
