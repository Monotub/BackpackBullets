using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] List<GameObject> itemList = new List<GameObject>();

    public static InventorySystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool TryAddItemToInventory(ItemBase item)
    {
        //TODO: Add conditions for when inventory is full. I'm assuming item will just disappear into the void
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                itemList.Add(item.gameObject);
                slot.AddItemToSlot(item);
                return true;
            }
        }
        Debug.Log("Item couldn't be added to Inventory");
        return false;
    }

    public void AddItemToInventoryListOnly(ItemBase item)
    {
            itemList.Add(item.gameObject);
    }

    public bool TryRemoveItemFromInventory(ItemBase item)
    {
        if (itemList.Contains(item.gameObject))
        {
            itemList.Remove(item.gameObject);
            return true;
        }
        else Debug.LogError("No such item found");

        return false;
    }
}
