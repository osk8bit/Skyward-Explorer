using Assets.Scripts.Components.Audio;
using Assets.Scripts.Components.ColliderBased;
using UnityEngine;

namespace Assets.Scripts.Components.Creature
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private bool _invertScale;
        [SerializeField] protected float _damageVelocity;

        [Header("Checkers")]
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] protected CheckCircleOverlap _attackRange;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        public bool IsGrounded;
        protected bool _isJumping;

        protected static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        protected static readonly int IsRunning = Animator.StringToHash("is-running");
        protected static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        protected static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack1");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        private void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity();
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsRunning, Direction.x != 0);

            UpdateSpriteDirection(Direction);
        }

        protected virtual float CalculateXVelocity()
        {
            return Direction.x * CalculateSpeed();
        }
        protected virtual float CalculateSpeed()
        {
            return _speed;
        }
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;

                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (IsGrounded)
            {
                yVelocity = _jumpSpeed;
                Sounds.Play("Jump");
            }
            return yVelocity;
        }
        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = _invertScale ? -1f : 1f;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1f, 1);

            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1f, 1);

            }
        }

        public virtual void TakeDamage()
        {
            Sounds.Play("Hit");
            _isJumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }
        public void OnDoAttack()
        {
            _attackRange.Check();
            Sounds.Play("Attack");
        }
    }
}
