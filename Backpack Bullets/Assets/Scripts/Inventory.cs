using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] List<InventoryItemData> itemList = new List<InventoryItemData>();
    [SerializeField] GameObject inventoryPanel;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
    }

    public bool TryAddItemToInventory(InventoryItemData item)
    {
        bool itemAdded = false;

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                itemList.Add(item);
                slot.AddItemToSlot(item);
                itemAdded = true;
                return itemAdded;
            }
        }

        return itemAdded;
        
    }

    public bool TryRemoveItemFromInventory(InventoryItemData item)
    {
        bool itemRemoved = false;

        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            return itemRemoved = true;
        }
        else Debug.LogError("No such item found");

        return itemRemoved;
    }
}
