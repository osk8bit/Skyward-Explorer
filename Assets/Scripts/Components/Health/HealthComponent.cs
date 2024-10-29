using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;
        public void ModifyHealth(float healthDelta)
        {
            if (_health <= 0) return;
            
            _health += healthDelta;


            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }

            if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }

        }

        public void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

    }
}
