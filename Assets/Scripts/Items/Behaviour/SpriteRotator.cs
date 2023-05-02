using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void FixedUpdate()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0, Space.Self);
    }
}
