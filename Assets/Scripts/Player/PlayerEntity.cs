using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace Player 
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private DirectionalMovementData _directionalMovementData;
        [SerializeField] private JumperData _jumperData;

        private DirectionMover _directionMover;
        private Jumper _jumper;

        [Header("Jump")]
        [SerializeField] private JumpPointController _jumpPointController;
        [SerializeField] private bool _isJumping;
        private Vector2 _wallJumpNormal;
        [SerializeField] private float _wallJumpForce;
        [SerializeField] private bool _wallrun;

        [SerializeField] private bool _isAttacking;

        private Rigidbody2D _rigidbody;

        [SerializeField] private Animator _animator;
        private AnimationType _currentAnimationType;

        [SerializeField] private Transform _playerCloseAttackPoint;
        [SerializeField] private float _playerCloseAttackRadius;
        [SerializeField] private float _playerCloseAttackDamage;
        private bool _isPlayerDead = false;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionMover = new DirectionMover(_rigidbody, _directionalMovementData);
            _jumper = new Jumper(_rigidbody, _jumpPointController, _jumperData);
        }
        private void Update()
        {
            GetIsJump();
            UpdateAnimations();

        }
        private void GetIsJump() { _isJumping = _jumpPointController.IsJumping(); }
        
        private void UpdateAnimations() 
        {
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Run, _directionMover.IsMoving);
            PlayAnimation(AnimationType.Jump, _isJumping);
            PlayAnimation(AnimationType.Climb, _wallrun && _isJumping);
            PlayAnimation(AnimationType.Attack, _isAttacking);
            PlayAnimation(AnimationType.Die, _isPlayerDead);
        }
        public void Attack() 
        {
            _isAttacking = true;
            Debug.Log("attack");
        }
        private void AttackStop()
        {
            _isAttacking = false;
        }
        public void MoveHorizontally(float direction) => _directionMover.MoveHorizontally(direction);
        public void Jump() => _jumper.Jump();
        private void OnCollisionStay2D(Collision2D collision)
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                Vector2 sum = new Vector2(transform.position.x, transform.position.y) + collision.contacts[i].normal;
                Vector2 contactDirection = new Vector2(sum.x - transform.position.x, sum.y - transform.position.y);
                float scalarProduct = contactDirection.x * Vector2.up.x + contactDirection.y * Vector2.up.y;
                if(scalarProduct == 0) 
                {
                    _wallJumpNormal = contactDirection;
                    _wallrun = true;
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _wallrun = false;
        }
        public void WallClimb() 
        {
            if (_wallrun && _isJumping) 
            {
                Vector2 WallRunDirection = _wallJumpNormal + Vector2.up;
                _rigidbody.AddForce(WallRunDirection * _wallJumpForce);
                _directionMover.Flip();
                _wallrun = false;
            }
        }
        private void PlayAnimation(AnimationType animationType, bool active) 
        {
            if (!active) 
            {
                if(_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType) 
                {
                    return;
                }
                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }
            if(_currentAnimationType > animationType) 
            {
                return;
            }
            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }
        private void PlayAnimation(AnimationType animationType) 
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
        private void PlayerCloseAttack() 
        {
            var targetCollider = Physics2D.OverlapCircle(_playerCloseAttackPoint.transform.position, _playerCloseAttackRadius);
            if (targetCollider != null && targetCollider.TryGetComponent(out IDamageble damageble)) { damageble.TakeDamage(_playerCloseAttackDamage); }
        }
        public void Die() 
        {
            _isPlayerDead = true;
        }


    }

}
