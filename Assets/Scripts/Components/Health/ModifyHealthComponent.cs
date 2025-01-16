using Assets.Scripts.Components.Creature.Hero;
using UnityEngine;

namespace Assets.Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;
        [SerializeField] private Hero _hero;

        public void SetDelta(int delta)
        {
            _hpDelta = delta;
        }

        public void ApplyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null && !_hero._imune)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }

    }
}
