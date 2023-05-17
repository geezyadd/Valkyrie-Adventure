
using System;
using UnityEngine;


[Serializable]
public class EnemyDescriptor 
{
    [field: SerializeField] public Rigidbody2D Rigidbody;
    [field: SerializeField] public float PatrolSizeSphere; 
    [field: SerializeField] public float AttackRangeDistance;
    [field: SerializeField] public float PatrolRange;
    [field: SerializeField] public float RotatePatrolDistance;
    [field: SerializeField] public Animator Animator;
}
