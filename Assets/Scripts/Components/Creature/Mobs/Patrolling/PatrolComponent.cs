using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.Mobs.Patrolling
{
    public abstract class PatrolComponent : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
