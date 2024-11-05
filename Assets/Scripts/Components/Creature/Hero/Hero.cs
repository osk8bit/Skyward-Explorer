using Assets.Scripts.Components.ColliderBased;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private int _rollForce;
        [SerializeField] private GameObject _shield;
        [SerializeField] private ColliderCheck _ceilingCheck;
        [SerializeField] private CheckCircleOverlap _interactionCheck;


        public bool _imune = false;
        private bool _isAttacked = false;
        public bool _isRolling;
        private float _timeSinceAttack = 0.0f;
        private int _currentAttack = 0;
        public bool IsCeiling;
        private int _currentCoin;

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

        private void Start()
        {
            _currentCoin = 0;
        }
        protected override void Update()
        {
            base.Update();

            IsCeiling = _ceilingCheck.IsTouchingLayer;
            _timeSinceAttack += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (_isRolling)
            {
                Rigidbody.velocity = Vector2.zero;

                if (transform.localScale.x < 0)
                {
                    Rigidbody.AddForce(Vector2.left * _rollForce);
                }
                else if (transform.localScale.x > 0)
                {
                    Rigidbody.AddForce(Vector2.right * _rollForce);
                }

            }
            else
            {
                var xVelocity = CalculateXVelocity();
                var yVelocity = CalculateYVelocity();
                Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
            }

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetBool(IsCeilingKey, IsCeiling);


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

        }
        public void RollWhileCeiling()
        {
            _isRolling = true;
        }

        public void StopBlock()
        {
            _shield.SetActive(false);
        }
        public void Roll()
        {
            if (IsGrounded)
            {
                _collider.isTrigger = true;
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

        public void Interact()
        {
            _interactionCheck.Check();
        }

        public void AddCoin()
        {
            _currentCoin += 1;
            Debug.Log($"Монетка собрана, в кошельке {_currentCoin} монет");
        }

    }
}
