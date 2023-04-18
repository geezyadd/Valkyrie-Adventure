using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position,_target.transform.position, Time.fixedDeltaTime * _speed);
    }
}
