using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class BossFloodState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<FloodSpikeController>();
            spawner.SpikeLevitate();
            var stopBlockSpawner = animator.GetComponent<BlockController>();
            stopBlockSpawner.StopImmune();
        }
    }
}
