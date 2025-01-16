using Assets.Scripts.Components.Model;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] public HealthChangeEvent _onChange;


        public int Health => _health;

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;
            
            _health += healthDelta;
            _onChange?.Invoke(_health);

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

        public void SetHealth(int health)
        {
            _health = health;
        }

        public void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}
