using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _yDistance;

   
 
    private void FixedUpdate()
    {
        
        transform.position = new Vector2(_target.position.x, _target.position.y + _yDistance);
        //transform.position = new Vector2(_target.position.x, transform.position.y);
    }
}
