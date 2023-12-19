
using Player;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ItemDescriptor _itemDescriptor;
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private TMP_Text _coinsCounterText;
    [SerializeField] private CoinsCounter _coinsCounter;

    public void SetItem(Sprite sprite, string itemName) 
    {
        _sprite.sprite = sprite;
        _text.text = itemName;
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            PickUpItem();
            switch (_itemDescriptor.ItemId)
            {
                case (ItemId)1: //heal
                    if (collision.gameObject.TryGetComponent(out IHeal heal)) { heal.Heal(_itemDescriptor.HurtHeal); }
                    break;
                case (ItemId)2: //shield
                    break;
                case (ItemId)3: //damage
                    _playerEntity.SetDamage(_itemDescriptor.DamageUp);
                    break;
                case (ItemId)4: //coins
                    _coinsCounter.CollectCoin();
                    _coinsCounterText.text = "Coins Collected: " + _coinsCounter.GetCurrentAmountOfCoins() + "/" + _coinsCounter.GetRequireAmountOfCoins();
                    break;

            }
        }
    }

    private void PickUpItem() 
    {
        DestroyItem();
    }
    private void DestroyItem() 
    {
        Destroy(gameObject);
    }
    
}
