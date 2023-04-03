using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointController : MonoBehaviour
{
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _wallrun;
    private Vector2 _wallJumpNormal;

    private void OnCollisionStay2D(Collision2D collision) 
    {
        _isJumping = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isJumping = true;
       // _wallrun = false;
    }

    
    public bool IsJumping() { return _isJumping; }
}
