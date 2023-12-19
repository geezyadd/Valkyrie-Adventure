using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemySpellController : MonoBehaviour
{
    [SerializeField] private DirectionalMovementData _directionalMovementData;
    private DirectionMover _directionMover;
    private GameObject _player;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _spellDamage;
    private float _direction;
    private Vector2 _startPosition;
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player");
        _startPosition = transform.position;
        _directionMover = new DirectionMover(_rigidbody, _directionalMovementData);
    }

    // Update is called once per frame
    private void Update()
    {
        _directionMover.MoveHorizontally(_direction);
        SetDirection();
    }
    private void SetDirection()
    {
        if (_player.transform.position.x < _startPosition.x)
        {
            TurnLeftDirection();
        }
        if (_player.transform.position.x > _startPosition.x)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(_spellDamage);
        }
        Destroy(gameObject);
    }
    
}
