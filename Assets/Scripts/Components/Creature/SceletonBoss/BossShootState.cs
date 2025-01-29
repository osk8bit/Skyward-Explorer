using Assets.Scripts.GoBased;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class BossShootState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.LaunchProjectiles();

            var immune = animator.GetComponent<BlockController>();
            immune.StopImmune();
        }
    }
}
