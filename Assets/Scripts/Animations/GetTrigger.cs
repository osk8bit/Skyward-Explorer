using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class GetTrigger : MonoBehaviour
    {
        [SerializeField] private string _trigger;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Get()
        {
            _animator.SetTrigger(_trigger);
        }
    }
}
