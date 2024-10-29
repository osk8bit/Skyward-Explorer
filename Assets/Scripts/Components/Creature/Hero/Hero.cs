using Assets.Scripts.Components.ColliderBased;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private int _rollForce;
        [SerializeField] private GameObject _shield;
        [SerializeField] private ColliderCheck _ceilingCheck;


        public bool _imune = false;
        private bool _isAttacked = false;
        public bool _isRolling;
        private float _timeSinceAttack = 0.0f;
        private int _currentAttack = 0;
        public bool IsCeiling;

        private Collider2D _collider;

        private static readonly int IsRolling = Animator.StringToHash("is_rolling");
        private static readonly int IsBlocked = Animator.StringToHash("is_blocked");
        private static readonly int IsIdleBlock = Animator.StringToHash("idleBlock");
        protected static readonly int IsCeilingKey = Animator.StringToHash("is-ceiling");

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
        }
        protected override void Update()
        {
            base.Update();
            if (_isRolling)
            {
                Rigidbody.velocity = Vector2.zero;
                if (!IsCeiling)
                    _collider.isTrigger = true;

                if (transform.localScale.x < 0)
                {
                    Rigidbody.AddForce(Vector2.left * _rollForce);
                }
                else if (transform.localScale.x > 0)
                {
                    Rigidbody.AddForce(Vector2.right * _rollForce);
                }

            }


            IsCeiling = _ceilingCheck.IsTouchingLayer;

            _timeSinceAttack += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (!_isRolling)
            {
                var xVelocity = CalculateXVelocity();
                var yVelocity = CalculateYVelocity();
                Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
            }
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsRunning, Direction.x != 0);



            UpdateSpriteDirection(Direction);
        }

        protected override float CalculateSpeed()
        {
            if (!_isAttacked)
                return _speed;

            return 0;

        }

        public override void Attack()
        {
            if (_timeSinceAttack > 0.25f && !_isRolling)
            {
                _isAttacked = true;
                _currentAttack++;

                if (_currentAttack > 3)
                    _currentAttack = 1;

                if (_timeSinceAttack > 1.0f)
                    _currentAttack = 1;

                Animator.SetTrigger("attack" + _currentAttack);
                _timeSinceAttack = 0.0f;
            }
        }

        public void StopAttack()
        {
            _isAttacked = false;
        }
        public void StopRoll()
        {
            _isRolling = false;
            _collider.isTrigger = false;
            Animator.SetBool(IsCeilingKey, IsCeiling);

            if (!_isRolling && IsCeiling)
            {
                RollWhileCeiling();
            }

        }
        public void RollWhileCeiling()
        {
            if (IsCeiling)
            {
                _isRolling = true;
            }
        }

        public void StopBlock()
        {
            _shield.SetActive(false);
        }
        public void Roll()
        {
            if (IsGrounded)
            {
                _isRolling = true;
                Animator.SetTrigger(IsRolling);
            }
        }

        public void Block()
        {
            if (!_isRolling)
            {
                Animator.SetTrigger(IsBlocked);
                _shield.SetActive(true);
                Animator.SetBool(IsIdleBlock, true);
                _imune = true;
            }
        }

        public void IdleBlock()
        {
            _imune = false;
            Animator.SetBool(IsIdleBlock, false);
        }

        public override void TakeDamage()
        {
            if (!_isRolling)
            {
                _isAttacked = false;
                _isJumping = false;
                Animator.SetTrigger(Hit);
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
            }
        }

    }
}
