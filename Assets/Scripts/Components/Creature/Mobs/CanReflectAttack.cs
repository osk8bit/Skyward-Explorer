using UnityEngine;

namespace Assets.Scripts.Components.Creature.Mobs
{
    public class CanReflectAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _vulnerability;

        public void SetActiveVulnerability()
        {
            _vulnerability.SetActive(true);
        }

        public void SetDisactiveVulnerability()
        {
            _vulnerability.SetActive(false);
        }
    }
}
