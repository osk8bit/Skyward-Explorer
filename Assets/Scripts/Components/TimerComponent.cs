using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private TimerData[] _timers;

        public void SetTimer(int index)
        {
            var timer = _timers[index];

            StartCoroutine(StartTimer(timer));
        }

        private IEnumerator StartTimer(TimerData timer)
        {
            yield return new WaitForSeconds(timer.Delay);

            timer.OnTimeUp?.Invoke();
        }

        [Serializable]
        public class TimerData
        {
            public float Delay;
            public UnityEvent OnTimeUp;
        }
    }
}
