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
    Transform inventoryParent;

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
        inventoryParent = FindObjectOfType<InventoryParent>().transform;
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
            if (PotionBar.Instance.TryAddToPotionBar(this))
            {
                transform.SetParent(inventoryParent);
                gameObject.SetActive(false);

            }
            else if (InventorySystem.Instance.TryAddToFirstAvailableSlot(this))
            {
                transform.SetParent(inventoryParent);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (InventorySystem.Instance.TryAddToFirstAvailableSlot(this))
            {
                transform.SetParent(inventoryParent);
                gameObject.SetActive(false);
            }
        }
    }
}
