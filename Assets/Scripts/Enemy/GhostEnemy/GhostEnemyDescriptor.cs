using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class GhostEnemyDescriptor
{
    [field: SerializeField] public Rigidbody2D Rigidbody;
    [field: SerializeField] public float PatrolSizeSphere;
    [field: SerializeField] public float PatrolRange;
    [field: SerializeField] public float RotatePatrolDistance;
    [field: SerializeField] public float _maxHitPoints;
    [field: SerializeField] public GameObject SpellPrephab;
    [field: SerializeField] public float SpellAttackDamage;
}







