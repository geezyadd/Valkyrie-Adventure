using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private Slider _slider;
    private float _currentHP;
    private void Start()
    {
        _currentHP = _hp;
    }
    private void Update()
    {
        _slider.value = _currentHP;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
    }
    
}
