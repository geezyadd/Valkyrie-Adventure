using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSpellEntity : MonoBehaviour
{
    [SerializeField] public Transform _rangeAttackPoint;
    [SerializeField] public float _rangeAttackDamage;
    [SerializeField] public float _rangeAttackRadius;
    [SerializeField] private GameObject _player;
    
    private Vector2 _spellSpawner;
    private void Awake()
    {
        _player = GameObject.Find("Player");
    }
    private void Start()
    {
        _spellSpawner = new Vector2(_player.transform.position.x, _player.transform.position.y + 0.9f);
        transform.position = _spellSpawner;
    }
    private void SpellDamager()
    {
        var targetCollider = Physics2D.OverlapCircle(_rangeAttackPoint.position, _rangeAttackRadius);
        if (targetCollider != null && targetCollider.TryGetComponent(out IDamageble damageble)) 
        {
            damageble.TakeDamage(_rangeAttackDamage);
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
