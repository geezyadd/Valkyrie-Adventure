using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    private float _currentAmountOfCoins;
    [SerializeField] private float _requiredNumberOfCoins;

    private void Start()
    {
        _currentAmountOfCoins = 0f;
    }
    public void CollectCoin() 
    {
        _currentAmountOfCoins++;
    }
    public float GetCurrentAmountOfCoins() { return _currentAmountOfCoins; }
    public float GetRequireAmountOfCoins() { return _requiredNumberOfCoins; }
}
