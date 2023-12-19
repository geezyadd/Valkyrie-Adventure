
using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class EnemyDescriptor 
{
    [field: SerializeField] public Rigidbody2D Rigidbody;
    [field: SerializeField] public float PatrolSizeSphere; 
    [field: SerializeField] public float AttackRangeDistance;
    [field: SerializeField] public float PatrolRange;
    [field: SerializeField] public float RotatePatrolDistance;
    [field: SerializeField] public Animator Animator;
    [field: SerializeField] public Slider _hpSlider;
    [field: SerializeField] public float _maxHitPoints;
    [field: SerializeField] public GameObject SpellPrephab;
    [field: SerializeField] public Transform CloseAttackPoint;
    [field: SerializeField] public float CloseAttackRadius;
    [field: SerializeField] public float CloseAttackDamage;
}
