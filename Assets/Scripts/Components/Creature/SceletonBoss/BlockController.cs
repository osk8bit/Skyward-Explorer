using Assets.Scripts.Components.Health;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class BlockController : MonoBehaviour
    {

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        public void MakeImmune()
        {
            _health.Immune.Retain(this);
        }

        public void StopImmune()
        {
            _health.Immune.Release(this);
        }
    }
}
