using System;
using UnityEngine;

[Serializable]
public class DirectionalMovementData 
{
    [field: SerializeField] public float _horizontalSpeed { get; private set; }
    [field: SerializeField] public bool FaceLeft { get; private set; }
}
