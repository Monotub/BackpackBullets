using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBar : MonoBehaviour
{
    // TODO: Create the ability to drag a potion from one slot to another


    [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] List<InventoryItemData> potionList = new List<InventoryItemData>();

    public static PotionBar Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    private void OnEnable()
    {
        PlayerControls.OnPotionKeyPress += ProcessPotionKey;
    }

    private void OnDisable()
    {
        PlayerControls.OnPotionKeyPress -= ProcessPotionKey;
    }

    void ProcessPotionKey(int key)
    {
        if(key == 0 && slots[0].MyItemData != null)
            slots[0].RemoveItemFromSlot();

        if (key == 1 && slots[1].MyItemData != null)
            slots[1].RemoveItemFromSlot();
    }

    public bool TryAddItemToPotionBar(InventoryItemData item)
    {
        bool itemAdded = false;

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                potionList.Add(item);
                slot.AddItemToSlot(item);
                itemAdded = true;
                return itemAdded;
            }
        }

        return itemAdded;
    }

    public bool TryRemoveItemFromPotionBar(InventoryItemData item)
    {
        bool itemRemoved = false;

        if (potionList.Contains(item))
        {
            potionList.Remove(item);
            return itemRemoved = true;
        }
        else Debug.LogError("No such item found");

        return itemRemoved;
    }
}
