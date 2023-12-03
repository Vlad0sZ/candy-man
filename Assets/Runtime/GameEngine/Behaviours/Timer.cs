using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours
{
    public class Timer : MonoBehaviour
    {
        
        [SerializeField] private float inEditorTimerSeconds;

        [SerializeField] private UnityEvent<float> onTimerTick = new();

        [SerializeField] private UnityEvent onTimerEnd = new();

        public UnityEvent<float> OnTimerTick => onTimerTick;

        public UnityEvent OnTimerEnd => onTimerEnd;

        private bool onPause;
        private Coroutine _timerCoroutine;


        [ContextMenu(nameof(StartTimer))]
        private void StartFromEditor() => 
            StartTimer(inEditorTimerSeconds);

        public void StartTimer(float seconds)
        {
            StopTimer();
            _timerCoroutine = StartCoroutine(DoTime(seconds));
        }

        [ContextMenu(nameof(StopTimer))]
        public void StopTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            onPause = false;
            OnTimerTick?.Invoke(0f);
        }

        [ContextMenu(nameof(PauseTimer))]
        public void PauseTimer()
        {
            if (!onPause && _timerCoroutine != null)
                onPause = true;
        }

        [ContextMenu(nameof(ContinueTimer))]
        public void ContinueTimer()
        {
            if (onPause && _timerCoroutine != null)
                onPause = false;
        }

        private IEnumerator DoTime(float total)
        {
            var time = 0f;
            do
            {
                if (onPause)
                {
                    yield return null;
                    continue;
                }

                time += Time.deltaTime;
                float p = time / total;
                
                onTimerTick?.Invoke(p);
                yield return null;
            } while (time <= total);

            onTimerEnd?.Invoke();
            _timerCoroutine = null;
        }
    }
}