using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinsCounter : MonoBehaviour
{
    private float _currentAmountOfCoins;
    [SerializeField] private float _requiredNumberOfCoins;

    private void Start()
    {
        _currentAmountOfCoins = 0f;
    }
    private void Update()
    {
        if(_currentAmountOfCoins == _requiredNumberOfCoins) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  }
    }
    public void CollectCoin() 
    {
        _currentAmountOfCoins++;
    }
    public float GetCurrentAmountOfCoins() { return _currentAmountOfCoins; }
    public float GetRequireAmountOfCoins() { return _requiredNumberOfCoins; }
}
