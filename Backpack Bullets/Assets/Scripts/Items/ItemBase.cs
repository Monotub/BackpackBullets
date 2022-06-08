using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class ItemBase : MonoBehaviour
{   [Header("Base Item Setup")]
    [SerializeField] InventoryItemData myData;
    
    SpriteRenderer spriteRend;
    string itemName;
    ItemType itemType;
    

    public InventoryItemData MyData => myData;
    public Sprite Sprite => spriteRend.sprite;
    public string ItemName => itemName;
    public ItemType ItemType => itemType;


    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = myData.Icon;
        itemName = myData.ItemName;
        itemType = myData.ItemType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PickupItem();
    }

    private void PickupItem()
    {
        if (itemType == ItemType.Potion)
        {
            if (PotionBar.Instance.TryAddItemToPotionBar(this))
                //Destroy(gameObject);
                gameObject.SetActive(false);
            else if (InventorySystem.Instance.TryAddItemToInventory(this))
                //Destroy(gameObject);
                gameObject.SetActive(false);
        }
        else
        {
            if (InventorySystem.Instance.TryAddItemToInventory(this))
                //Destroy(gameObject);
                gameObject.SetActive(false);
        }
    }
}
