using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours.UI
{
    public class StressBar : MonoBehaviour
    {

        [SerializeField] private UnityEvent<float> onProgress = new();
        public UnityEvent<float> OnProgress => onProgress;

        private int _maxValue;
        private int _current;

        public void Init(int maxValue)
        {
            _maxValue = maxValue;
            _current = 0;
            OnProgress?.Invoke(0);
        }

        public void Increment(int value)
        {
            _current = Mathf.Clamp(_current + value, 0, _maxValue);
            var progress = (float)_current / _maxValue;
            onProgress?.Invoke(progress);
        }
    }
}