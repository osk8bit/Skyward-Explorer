using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MiniGames.Roots
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private int switchCount;
        [SerializeField] private UnityEvent _action;

        static public Main Instance;
        private int onCount = 0;

        private void Awake()
        {
            Instance = this;
        }
        public void SwitchChange(int points)
        {
            onCount = onCount + points;
            if (onCount == switchCount)
            {
                _action.Invoke();
            }
        }
    }
}
