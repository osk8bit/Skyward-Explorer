using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class BossBlockState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<BlockController>();
            spawner.MakeImmune();
        }
    }
}
