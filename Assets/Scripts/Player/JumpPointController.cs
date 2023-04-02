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
        //for (int i = 0; i < collision.contacts.Length; i++)
        //{
        //    Vector2 sum = new Vector2(transform.position.x, transform.position.y) + collision.contacts[i].normal;
        //    Vector2 contactDirection = new Vector2(sum.x - transform.position.x, sum.y - transform.position.y);
        //    float scalarProduct = contactDirection.x * Vector2.up.x + contactDirection.y * Vector2.up.y;
        //    if (scalarProduct == 0)
        //    {
        //        _wallJumpNormal = contactDirection;
        //        _wallrun = true;
        //    }
        //}
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isJumping = true;
       // _wallrun = false;
    }

    
    public bool IsJumping() { return _isJumping; }
    //public bool IsWallrun() { return _wallrun; }
    //public Vector2 GetWallJumpNormal() { return _wallJumpNormal; }
}
