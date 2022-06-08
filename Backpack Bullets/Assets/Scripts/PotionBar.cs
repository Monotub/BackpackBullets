using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PotionBar : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] List<ItemBase> potionList = new List<ItemBase>();

    public static PotionBar Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    private void OnEnable() { PlayerControls.OnPotionKeyPress += ProcessPotionKey; }
    private void OnDisable() { PlayerControls.OnPotionKeyPress -= ProcessPotionKey; }

    void ProcessPotionKey(int key)
    {
        if(key == 0 && slots[0].MyItemData != null)
            slots[0].RemoveItemFromSlot();

        if (key == 1 && slots[1].MyItemData != null)
            slots[1].RemoveItemFromSlot();
    }

    public bool TryAddItemToPotionBar(ItemBase item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                potionList.Add(item);
                slot.AddItemToSlot(item);
                return true;
            }
        }

        return false;
    }

    public void AddItemToPotionListOnly(ItemBase item)
    {
        if(item.ItemType == ItemType.Potion)
            potionList.Add(item);
    }

    public bool TryRemoveItemFromPotionBar(ItemBase item)
    {
        bool itemRemoved = false;

        if (potionList.Contains(item) && !itemRemoved)
        {
            potionList.Remove(item);
            itemRemoved = true;
            return true;
        }

        return false;
    }
}
