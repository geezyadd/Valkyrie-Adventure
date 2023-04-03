using UnityEngine;

public class Jumper 
{
    private Rigidbody2D _rigidbody;
    private JumpPointController _jumpPointController;
    private JumperData _jumperData;
    public Jumper(Rigidbody2D rigidbody, JumpPointController jumpPointController, JumperData jumperData) 
    {
        _rigidbody= rigidbody;
        _jumpPointController = jumpPointController;
        _jumperData = jumperData;

    }

    public void Jump()
    {
        if (_jumpPointController.IsJumping())
        {
            return;
        }
        _rigidbody.AddForce(Vector2.up * _jumperData.JumpForce);
    }
}
