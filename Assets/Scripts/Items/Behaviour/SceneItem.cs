
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
        }
    }

    private void PickUpItem() 
    {
        //Debug.Log(_itemDescriptor.ItemType);
        DestroyItem();
    }
    private void DestroyItem() 
    {
        Destroy(gameObject);
    }
    
}
