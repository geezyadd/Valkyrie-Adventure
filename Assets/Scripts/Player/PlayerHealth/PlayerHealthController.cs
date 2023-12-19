using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour, IDamageble, IHeal
{
    [SerializeField] private float _hp;
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerEntity _playerEntity;
    private float _currentHP;
    public void Heal(float heal)
    {
        if (_hp < _currentHP + heal)
        {
            _currentHP = _hp;
        }
        else
        {
            _currentHP += heal;
        }
    }
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
        if(_currentHP < 1 ) 
        { 
            _playerEntity.Die();
        }
    }
    public void ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

}
