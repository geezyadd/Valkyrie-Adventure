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
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isJumping;

        private Rigidbody2D _rigidbody;

        [SerializeField] private Animator _animator;
        private Vector2 _movement;
        private AnimationType _currentAnimationType;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            UpdateAnimations();
        }
        private void UpdateAnimations() 
        {
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Run, _movement.magnitude > 0);
            PlayAnimation(AnimationType.Jump, _isJumping);
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
            _isJumping = false;
            
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _isJumping = true;
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
