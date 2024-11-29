using Assets.Scripts.Components.ColliderBased;
using Assets.Scripts.Components.Health;
using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Data;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.Hero
{
    public class Hero : Creature, ICanAddInInventory
    {
        [SerializeField] private int _rollForce;
        [SerializeField] private GameObject _shield;
        [SerializeField] private ColliderCheck _ceilingCheck;
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private float _blockDuration = 2.0f;  // Длительность блока
        [SerializeField] private float _blockCooldown = 5.0f;  // Время перезарядки блока

        
        public bool _imune = false;
        private bool _isAttacked = false;
        public bool _isRolling;
        private float _timeSinceAttack = 0.0f;
        private int _currentAttack = 0;
        public bool IsCeiling;
        private bool _canBlock = true;
        private bool _isHoldingBlock; // Удерживается ли кнопка блока
        private bool _isBlockActive;  // Активен ли блок
        private float _originalSpeed;
        private bool _canAttack = true;
        
        private Collider2D _collider;
        private HealthComponent _health;
        private GameSession _session;

        //private int CoinsCount => _session.Data.Inventory.Count("Coin");

        private static readonly int IsRolling = Animator.StringToHash("is_rolling");
        private static readonly int IsBlocked = Animator.StringToHash("is_blocked");
        private static readonly int IsIdleBlock = Animator.StringToHash("idleBlock");
        protected static readonly int IsCeilingKey = Animator.StringToHash("is-ceiling");

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
            _originalSpeed = _speed;
        }

        private void Start()
        {
            _session = GameSession.Instance;
            _health = GetComponent<HealthComponent>();
            _health.SetHealth(_session.Data.Hp.Value);

        }
        protected override void Update()
        {
            base.Update();

            IsCeiling = _ceilingCheck.IsTouchingLayer;
            _timeSinceAttack += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (_isRolling )
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

        protected override float CalculateYVelocity()
        {

            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing && !_isBlockActive)
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

        public override void Attack()
        {
            if (_timeSinceAttack > 0.25f && !_isRolling && _canAttack)
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

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
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

        
        public void Roll()
        {
            if (IsGrounded && !_isBlockActive)
            {
                _collider.isTrigger = true;
                _isRolling = true;
                Animator.SetTrigger(IsRolling);
            }
        }

        public void Block()
        {
            if (_canBlock && !_isRolling)
            {
                _canAttack = false;
                Animator.SetTrigger(IsBlocked);
                _shield.SetActive(true);
                _imune = true;
                _isHoldingBlock = true;
                _isBlockActive = true;

                StartCoroutine(BlockCooldowRoutine());
            }
        }

        public void PressureCheck()
        {
            // Проверяем, удерживается ли кнопка блока
            if (_isHoldingBlock)
            {
                IdleBlock(); // Переходим в состояние IdleBlock
            }
            else
            {
                StopBlock(); // Заканчиваем блок
            }
        }


        private IEnumerator BlockDurationRoutine()
        {
            _isAttacked = false;
            // Держим блок в течение _blockDuration
            yield return new WaitForSeconds(_blockDuration);
            StopBlock();
            // Деактивируем блок
        }

        private IEnumerator BlockCooldowRoutine()
        {
            _canBlock = false;
            // Ждем окончания перезарядки
            yield return new WaitForSeconds(_blockCooldown);
            _canBlock = true;  // Позволяем снова использовать блок
        }


        public void IdleBlock()
        {
            if (_isBlockActive)
            {
                _canAttack = false;
                _shield.SetActive(false);
                Animator.SetBool(IsIdleBlock, true);
                _speed = 0;  // Отключаем движение
                StartCoroutine(BlockDurationRoutine());
                StartCoroutine(BlockCooldowRoutine());
            }
                
        }

        public void StopBlock()
        {
            _shield.SetActive(false);
            _imune = false;
            Animator.SetBool(IsIdleBlock, false);
            _speed = _originalSpeed;  // Возвращаем изначальную скорость
            _isBlockActive = false;
            _canAttack = true;
        }

        public void StopHoldingBlock()
        {
            _isHoldingBlock = false;
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
    }
}
