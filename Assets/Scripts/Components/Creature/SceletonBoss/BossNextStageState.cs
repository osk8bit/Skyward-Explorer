using Assets.Scripts.GoBased;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class BossNextStageState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.Stage++;

            var spikeSpawner = animator.GetComponent<FloodSpikeController>();
            spikeSpawner.Spawn();

            var immune = animator.GetComponent<BlockController>();
            immune.MakeImmune();
        }
    }
}
