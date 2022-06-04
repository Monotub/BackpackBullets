using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] InventoryItemData myData;
    [SerializeField] public int currentStack = 0;
    [SerializeField] int maxStackSize;

    public InventoryItemData MyData => myData;
    public int MaxStackSize => maxStackSize;
    public Sprite Sprite => sprite.sprite;
    
    SpriteRenderer sprite;
    string itemName;
    int id;



    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = myData.Icon;

        id = myData.Id;
        itemName = myData.ItemName;
        maxStackSize = myData.MaxStackSize;
        if (currentStack == 0) currentStack = 1;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(Inventory.Instance.TryAddItemToInventory(myData))
                Destroy(gameObject);
        }
    }

    
}
