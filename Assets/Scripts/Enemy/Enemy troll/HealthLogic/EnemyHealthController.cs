using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamageble
{
    private EnemyDescriptor _enemyDescriptor;
    private float _hp;
    private Slider _slider;
    private float _currentHP;
    private void Start()
    {
        _slider = _enemyDescriptor._hpSlider;
        _hp = _enemyDescriptor._maxHitPoints;
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
