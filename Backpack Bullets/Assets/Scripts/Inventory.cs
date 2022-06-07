using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] List<InventoryItemData> itemList = new List<InventoryItemData>();

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool TryAddItemToInventory(InventoryItemData item)
    {
        //TODO: Add conditions for when inventory is full. I'm assuming item will just disappear into the void
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                itemList.Add(item);
                slot.AddItemToSlot(item);
                return true;
            }
        }

        return false;
    }

    public void AddItemToInventoryListOnly(InventoryItemData item)
    {
            itemList.Add(item);
    }

    public bool TryRemoveItemFromInventory(InventoryItemData item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            return true;
        }
        else Debug.LogError("No such item found");

        return false;
    }
}
