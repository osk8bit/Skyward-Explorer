using Assets.Scripts.Components.ColliderBased;
using Assets.Scripts.Components.Creature.Mobs.Patrolling;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.Mobs
{
    public class MobAi : MonoBehaviour
    {
        [SerializeField] protected ColliderCheck _vision;
        [SerializeField] protected ColliderCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] protected float _missCooldown = 0.5f;

        [SerializeField] protected float _horizontalTrashold = 0.2f;

        private IEnumerator _current;
        protected GameObject _target;

        private bool _isDead;
        protected bool _onAttack;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        protected Creature _creature;
        protected Animator _animator;
        private PatrolComponent _patrol;
        protected virtual void Awake()
        {
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<PatrolComponent>();
        }
        protected virtual void Start()
        {
            ChangeStateTo(_patrol.DoPatrol());
        }
        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;
            _target = go;

            ChangeStateTo(AgroToHero());
        }

        protected IEnumerator AgroToHero()
        {
            LookAtHero();
            yield return new WaitForSeconds(_alarmDelay);

            ChangeStateTo(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        protected virtual IEnumerator GoToHero()
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

            ChangeStateTo(_patrol.DoPatrol());
        }
        protected virtual IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            LookAtHero();
            ChangeStateTo(GoToHero());
        }

        protected void SetDirectionToTarget()
        {
            _creature.SetDirection(GetDirectionToTarget());
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        protected void ChangeStateTo(IEnumerator coroutine)
        {

            _creature.SetDirection(Vector2.zero);
            if (_current != null)
            {
                StopCoroutine(_current);
            }

            _current = coroutine;
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {

            _isDead = true;
            _animator.SetBool(IsDeadKey, _isDead);

            _creature.SetDirection(Vector2.zero);
            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }
    }
}
