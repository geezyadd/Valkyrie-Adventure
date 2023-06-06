using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.ParticleSystem;

public class BaseEnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemyDescriptor _enemyDescriptor;
    [SerializeField] private DirectionalMovementData _directionalMovementData;
    private DirectionMover _directionMover;
    private Rigidbody2D _rigidbody;
    private Vector2 _startPosition;
    [SerializeField] private float _direction;
    private GameObject _player;
    private SimplEnemyAnimationType _currentAnimationType;
    private float _stopingDistance;
    public bool _isCloseAttacking;
    [SerializeField] public bool _isRangeAttacking;
    [SerializeField] public float _attackModifier;
    private bool _isEnemyDie = false;

    public void Die() 
    { 
        _isEnemyDie = true;
       
    }
    
    private void Start()
    {
        _player = GameObject.Find("Player");
        _direction = UnityEngine.Random.value < 0.5f ? Vector2.left.x : Vector2.right.x;
        _startPosition = transform.position;
        _rigidbody = _enemyDescriptor.Rigidbody;
        _directionMover = new DirectionMover(_rigidbody, _directionalMovementData);
        AttackModifier();
        
    }
    private void Update()
    {
        PlayerChecker();
        UpdateAnimations();
        if (_isEnemyDie)
        {
            StopMoving();
        }
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
        PlayAnimation(SimplEnemyAnimationType.Attack, _isCloseAttacking);
        PlayAnimation(SimplEnemyAnimationType.Cast, _isRangeAttacking);
        PlayAnimation(SimplEnemyAnimationType.Death, _isEnemyDie);
    }
    private void PlayerChecker() 
    {
        bool IsPlayerOnPatrolRange = Physics2D.OverlapCircle(_startPosition, _enemyDescriptor.PatrolSizeSphere, LayerMask.GetMask("Player"));
        if (IsPlayerOnPatrolRange) 
        {
            switch (_attackModifier) 
            {
                case 1:
                    CloseAttackMove();
                    break;
                case 2:
                    RangeAttackMove();
                    break;
            }
        }
        if (!IsPlayerOnPatrolRange) 
        {
            Patrol();
            _isCloseAttacking = false;
            _isRangeAttacking = false;
        }
    }
    private void AttackModifier() 
    {
        _attackModifier = UnityEngine.Random.value < 0.7f ? 1 : 2;
    }
    private void StopMoving() 
    {
        _directionMover.MoveHorizontally(Vector2.zero.x);
    }
    private void RangeAttackMove() 
    {
        if (_player.transform.position.x < transform.position.x)
        {
            TurnLeftDirection();
            StopMoving();
        }
        if (_player.transform.position.x > transform.position.x)
        {
            TurnRightDirection();
            StopMoving();
        }
        _isRangeAttacking = true;
        _isCloseAttacking = false;
    }
    private void SpawnSpellPrephab() { Instantiate(_enemyDescriptor.SpellPrephab); }
    private void CloseAttackMove() 
    {
        if (_player.transform.position.x < transform.position.x)
        {
            TurnLeftDirection();
        }
        if (_player.transform.position.x > transform.position.x)
        {
            TurnRightDirection();
        }
        if (Vector2.Distance(_player.transform.position, transform.position) < _enemyDescriptor.AttackRangeDistance)
        {
            StopMoving();
            _isCloseAttacking = true;
            _isRangeAttacking = false;
        }
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
    
    private void CloseAttack() 
    {
        var targetCollider = Physics2D.OverlapCircle(_enemyDescriptor.CloseAttackPoint.transform.position, _enemyDescriptor.CloseAttackRadius);
        if (targetCollider != null && targetCollider.TryGetComponent(out IDamageble damageble)) { damageble.TakeDamage(_enemyDescriptor.CloseAttackDamage); }  
    }
    private void Destroy() 
    {
        Destroy(gameObject);
    }
}
