using System.Collections;
using System.Linq;
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
        Potion[] temp = FindObjectsOfType<Potion>(true);    // TRUE allows it to find inactive objects!
        foreach (var thing in temp)
        {
            if (thing.name == slots[key].MyItemData.name)
                Destroy(thing.gameObject);
        }

        if (slots[key].MyItemData != null)
            slots[key].RemoveItemFromSlot();
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
