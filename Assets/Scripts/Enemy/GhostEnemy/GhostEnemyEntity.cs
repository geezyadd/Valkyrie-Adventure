using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyEntity : MonoBehaviour
{
    [SerializeField] private GhostEnemyDescriptor _enemyDescriptor;
    [SerializeField] private DirectionalMovementData _directionalMovementData;
    private DirectionMover _directionMover;
    private Rigidbody2D _rigidbody;
    private Vector2 _startPosition;
    [SerializeField] private float _direction;
    private GameObject _player;
    public bool _isCloseAttacking;
    [SerializeField] public bool _isRangeAttacking;
    [SerializeField] public float _attackModifier;
    [SerializeField] private bool _isEnemyDie = false;
    [SerializeField] private GameObject _spellSpawnPoint;
    private float _spellSpawnInterval = 2.0f;
    private bool _isAttacking;

    public void Die()
    {
        Destroy();

    }

    private void Start()
    {
        _player = GameObject.Find("Player");
        _direction = Random.value < 0.5f ? Vector2.left.x : Vector2.right.x;
        _startPosition = transform.position;
        _rigidbody = _enemyDescriptor.Rigidbody;
        _directionMover = new DirectionMover(_rigidbody, _directionalMovementData);
    }
    private void Update()
    {
        PlayerChecker();
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
    
    private void PlayerChecker()
    {
        bool IsPlayerOnPatrolRange = Physics2D.OverlapCircle(_startPosition, _enemyDescriptor.PatrolSizeSphere, LayerMask.GetMask("Player"));
        //Debug.Log(IsPlayerOnPatrolRange);
        if (IsPlayerOnPatrolRange)
        {
            RangeAttackMove();
        }
        if (!IsPlayerOnPatrolRange)
        {
            Patrol();
            _isAttacking = true;
            StopAllCoroutines();

        }
    }
    
    private void StopMoving()
    {
        _directionMover.MoveHorizontally(Vector2.zero.x);
    }
    private void RangeAttackMove()
    {
        if (_player.transform.position.x < transform.position.x)
        {
            if (_isAttacking) { StartCoroutine(StartSpawning()); }
            TurnLeftDirection();
            StopMoving();
        }
        if (_player.transform.position.x > transform.position.x)
        {
            if (_isAttacking) { StartCoroutine(StartSpawning()); }
            TurnRightDirection();
            StopMoving();
        }
    }
    private void SpawnSpellPrephab() 
    {
        Instantiate(_enemyDescriptor.SpellPrephab, _spellSpawnPoint.transform.position, Quaternion.identity);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    IEnumerator StartSpawning()
    {
        while (true)
        {
            SpawnSpellPrephab();
            _isAttacking = false;
            yield return new WaitForSeconds(_spellSpawnInterval);
        }
    }
}
