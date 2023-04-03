using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMover 
{
    private readonly Rigidbody2D _rigidbody;
    private readonly Transform _transform;
    private readonly DirectionalMovementData _directionalMovementData;
    private Vector2 _movement;
    public bool FacingLeft { get; private set; }
    public bool IsMoving => _movement.magnitude > 0;

    public DirectionMover(Rigidbody2D rigidbody, DirectionalMovementData directionalMovementData) 
    {
        _rigidbody= rigidbody;
        _transform= rigidbody.transform;
        _directionalMovementData = directionalMovementData;
    }

    public void MoveHorizontally(float direction)
    {
        _movement.x = direction;
        SetDirection(direction);
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _directionalMovementData.HorizontalSpeed;
        _rigidbody.velocity = velocity;
    }

    private void SetDirection(float direction)
    {
        
        if ((!FacingLeft && direction < 0) ||
            (FacingLeft && direction > 0))
        {
            Flip();
        }
    }

    public void Flip()
    {
        _transform.Rotate(0, 180, 0);
        FacingLeft = !FacingLeft;
    }
}
