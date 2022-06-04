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

        if (itemList.Contains(item))
        {
            Debug.Log($"{item} FOUND in inventory");
            if(item.MaxStackSize > 1)
            {
                foreach(var slot in slots)
                {
                    if (slot.MyItemData == item && slot.currentStackSize < item.MaxStackSize)
                    {
                        Debug.Log("Slot found with available stack");
                        itemList.Add(item);
                        slot.currentStackSize++;
                        itemAdded = true;
                        return itemAdded;
                    }
                    else
                    {
                        // Need to be able to add item to empty slot if current slot reaches max stack size
                    }
                }
            }
            else
            {
                AddItemToEmptySlot(item);
                itemAdded = true;
            }
            return itemAdded;
        }
        else
        {
            Debug.Log($"{item} NOT FOUND in inventory");
            AddItemToEmptySlot(item);
            itemAdded = true;
            return itemAdded;
        }
    }

    public bool TryRemoveItemFromInventory(InventoryItemData item)
    {
        bool itemRemoved = false;

        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            return itemRemoved = true;
        }
        else Debug.Log("No such item found");

        return itemRemoved;
    }

    void AddItemToEmptySlot(InventoryItemData item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                Debug.Log($"{item} added to empty inventory slot");
                itemList.Add(item);
                slot.AddItemToSlot(item);
                return;
            }
        }
    }
}
