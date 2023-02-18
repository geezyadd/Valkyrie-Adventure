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
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
  
        public void MoveHorizontally(float direction) 
        {
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
            Debug.Log("jump");
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            _isJumping = false;
            Debug.Log(collision.gameObject.name);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _isJumping = true;
            Debug.Log(collision.gameObject.name + "exit");
        }
       
    }

}
