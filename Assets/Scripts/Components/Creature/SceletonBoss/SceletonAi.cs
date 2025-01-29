using Assets.Scripts.Components.Creature.Mobs;
using Assets.Scripts.GoBased;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class SceletonAi : MobAi
    {
        [SerializeField] private SpawnComponent _shootTarget;
        [SerializeField] private float _shootCooldown = 0.5f;

        protected override void Awake()
        {
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>(); 
           
        }
        protected override void Start()
        {
            ChangeStateTo(WaitForHero());
        }

        private IEnumerator WaitForHero()
        {
            while (!_vision.IsTouchingLayer)
            {
                yield return null;
            }

            _target = _vision.GetTouchingObject();
            ChangeStateTo(AgroToHero());
        }

        public void DisableComponent()
        {
            enabled = false;
        }

        protected override IEnumerator GoToHero()
        {

            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    ChangeStateTo(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= _horizontalTrashold)
                        _creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }

                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(_missCooldown);
            ChangeStateTo(RangeAttack());
        }

        private IEnumerator RangeAttack()
        {
            if (_shootTarget == null)
            {
                Debug.LogError("SpawnComponent (_shootTarget) is not assigned or is missing in the inspector.", this);
                yield break;
            }

            while (!_vision.IsTouchingLayer)
            {
                _animator?.SetTrigger("range_attack");
                yield return new WaitForSeconds(_shootCooldown);
            }

            
            ChangeStateTo(GoToHero());

        }

        public void SpawnProjectile()
        {
            var projectile = _shootTarget.SpawnInstance();
            if (projectile == null)
            {
                Debug.LogError("Failed to spawn projectile. Ensure the prefab and target are assigned correctly.", this);
                return;
            }
        }

    }
}
