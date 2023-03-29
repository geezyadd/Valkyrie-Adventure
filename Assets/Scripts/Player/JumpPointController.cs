using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointController : MonoBehaviour
{
    [SerializeField] private bool _isJumping;
    
    private void OnCollisionStay2D(Collision2D collision) 
    {
        _isJumping = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isJumping = true;
    }
    public bool IsJumping() { return _isJumping; }
}
