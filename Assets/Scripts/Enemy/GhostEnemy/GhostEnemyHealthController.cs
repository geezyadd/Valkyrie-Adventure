using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostEnemyHealthController : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    [SerializeField] private Slider _slider;
    [SerializeField] private GhostEnemyEntity _ghostEnemyEntity;
    [SerializeField] private float _currentHP;
    private void Start()
    {
        _currentHP = _hp;
    }
    private void Update()
    {
        _slider.value = _currentHP;
        PulseCheck();
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
    }


    private void PulseCheck() 
    {
        if (_currentHP < 1) 
        {
            _ghostEnemyEntity.Die(); 
        } 
    }
}
