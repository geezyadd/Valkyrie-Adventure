using System;
using UnityEngine;

[Serializable]
public class DirectionalMovementData 
{
    [field: SerializeField] public float HorizontalSpeed { get; private set; }
    [field: SerializeField] public bool FaceLeft { get; private set; }
}
