using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BaseEnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemyDescriptor _enemyDescriptor;
    [SerializeField] private DirectionalMovementData _directionalMovementData;
    private DirectionMover _directionMover;
    private Rigidbody2D _rigidbody;
    private Vector2 _startPosition;
    [SerializeField] private float _direction;
    [SerializeField] private GameObject _player;
    private SimplEnemyAnimationType _currentAnimationType;
    private float _stopingDistance;
    public bool _isAttacking;

    private void Start()
    {
        _direction = Random.value < 0.5f ? Vector2.left.x : Vector2.right.x;
        _startPosition = transform.position;
        _rigidbody = _enemyDescriptor.Rigidbody;
        _directionMover = new DirectionMover(_rigidbody, _directionalMovementData);
    }
    private void Update()
    {
        PlayerChecker();
        UpdateAnimations();
    }
    private void Patrol() 
    {
       _directionMover.MoveHorizontally(_direction);
        if (transform.position.x > _startPosition.x + _enemyDescriptor.PatrolRange)
        {
            TurnLeftDirection();
        }
        if (transform.position.x < _startPosition.x - _enemyDescriptor.PatrolRange)
        {
            TurnRightDirection();
        }

    }

    private void TurnRightDirection() 
    {
        _direction = Vector2.right.x;
        _directionMover.MoveHorizontally(_direction);
    }
    private void TurnLeftDirection()
    {
        _direction = Vector2.left.x;
        _directionMover.MoveHorizontally(_direction);
    }
    private void UpdateAnimations()
    {
        PlayAnimation(SimplEnemyAnimationType.Idle, true);
        PlayAnimation(SimplEnemyAnimationType.Walk, _directionMover.IsMoving);
        //PlayAnimation(SimplEnemyAnimationType.Jump, _isJumping);
        //PlayAnimation(SimplEnemyAnimationType.Climb, _wallrun && _isJumping);
        PlayAnimation(SimplEnemyAnimationType.Attack, _isAttacking);
    }
    private void PlayerChecker() 
    {
        bool IsPlayerOnPatrolRange = Physics2D.OverlapCircle(_startPosition, _enemyDescriptor.PatrolSizeSphere, LayerMask.GetMask("Player"));
        Debug.Log(IsPlayerOnPatrolRange);
        if (IsPlayerOnPatrolRange) 
        {
            if(_player.transform.position.x < transform.position.x) 
            {
                TurnLeftDirection();
            }
            if (_player.transform.position.x > transform.position.x)
            {
                TurnRightDirection();
            }
            if (Vector2.Distance(_player.transform.position, transform.position) < _enemyDescriptor.AttackRangeDistance) 
            {
                _directionMover.MoveHorizontally(Vector2.zero.x);
                _isAttacking = true;
            }
        }
        if (!IsPlayerOnPatrolRange) { Patrol(); }
    }
    
    private void PlayAnimation(SimplEnemyAnimationType animationType, bool active)
    {
        if (!active)
        {
            if (_currentAnimationType == SimplEnemyAnimationType.Idle || _currentAnimationType != animationType)
            {
                return;
            }
            _currentAnimationType = SimplEnemyAnimationType.Idle;
            PlayAnimation(_currentAnimationType);
            return;
        }
        if (_currentAnimationType > animationType)
        {
            return;
        }
        _currentAnimationType = animationType;
        PlayAnimation(_currentAnimationType);
    }
    private void PlayAnimation(SimplEnemyAnimationType animationType)
    {
        _enemyDescriptor.Animator.SetInteger(nameof(SimplEnemyAnimationType), (int)animationType);
    }


}
