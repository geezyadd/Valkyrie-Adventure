using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace Player 
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")]
        [SerializeField] private JumpPointController _jumpPointController;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isJumping;
        private Vector2 _wallJumpNormal;
        [SerializeField] private float _wallJumpForce;
        [SerializeField] private bool _wallrun;

        [SerializeField] private bool _isAttacking;

        private Rigidbody2D _rigidbody;

        [SerializeField] private Animator _animator;
        private Vector2 _movement;
        private AnimationType _currentAnimationType;

        private Vector2 _normal;
        


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            _isJumping = _jumpPointController.IsJumping();
            UpdateAnimations();
        }
        private void UpdateAnimations() 
        {
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Run, _movement.magnitude > 0);
            PlayAnimation(AnimationType.Jump, _isJumping);
            PlayAnimation(AnimationType.Climb, _wallrun && _isJumping);
            PlayAnimation(AnimationType.Attack, _isAttacking);
        }
        public void Attack() 
        {
            _isAttacking = true;
        }
        private void AttackStop()
        {
            _isAttacking = false;
        }

        public void MoveHorizontally(float direction) 
        {
            _movement.x = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }
        
        
        private void SetDirection(float direction) 
        {
            if((_faceRight && direction < 0) ||
                (!_faceRight && direction > 0)) 
            {
                Flip();
            }
        }
        private void Flip() 
        {
            transform.Rotate(0,180,0);
            _faceRight = !_faceRight;
        }
        
        public void Jump() 
        {
            if (_isJumping) 
            {
                return;
            }
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }

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
                Flip();
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


    }

}
