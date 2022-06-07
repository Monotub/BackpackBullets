using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class InventorySlot : MonoBehaviour
{
    [SerializeField] InventoryItemData myItemData;
    [SerializeField] SlotType slotType;

    MouseSlot mouseSlot;
    Image image;
    bool isEmpty = true;

    public bool IsEmpty => isEmpty;
    public InventoryItemData MyItemData => myItemData;


    private void Awake()
    {
        image = GetComponent<Image>();
        mouseSlot = FindObjectOfType<MouseSlot>();
    }

    public void AddItemToSlot(InventoryItemData item)
    {
        if (item == null) return;
        
        myItemData = item;
        image.sprite = item.Icon;
        isEmpty = false;
    }

    public void RemoveItemFromSlot()
    {
        if (slotType == SlotType.Potion)
        {
            PotionBar.Instance.TryRemoveItemFromPotionBar(myItemData);
            ClearSlotData();
        }
        else
        {
            Inventory.Instance.TryRemoveItemFromInventory(myItemData);
            ClearSlotData();
        }
    }

    public void SlotClicked()
    {
        if (!UIManager.Instance.IsInventoryOpen) return;

        InventoryItemData mouseItemData = mouseSlot.MouseData;

        if (myItemData != null && mouseItemData == null)
        {
            // Transfer item to Mouse from Inventory slot
            mouseSlot.AddItemToMouse(myItemData);
            RemoveItemFromSlot();
            return;
        }
        else if (myItemData == null && mouseItemData != null)
        {
            // Transfer item to Inventory slot from Mouse
            switch (slotType)
            {
                case SlotType.Potion:
                    if (mouseItemData.ItemType == ItemType.Potion)
                    {
                        PotionBar.Instance.AddItemToPotionListOnly(mouseItemData);
                        AddItemToSlot(mouseItemData);
                    }
                    else
                        Inventory.Instance.TryAddItemToInventory(mouseItemData);
                    break;

                case SlotType.MainHand:
                    TryAddItemToGearSlot(ItemType.MainHand);
                    break;

                case SlotType.OffHand:
                    TryAddItemToGearSlot(ItemType.OffHand);
                    break;

                case SlotType.Head:
                    TryAddItemToGearSlot(ItemType.HeadGear);
                    break;

                case SlotType.Chest:
                    TryAddItemToGearSlot(ItemType.ChestGear);
                    break;

                case SlotType.Boots:
                    TryAddItemToGearSlot(ItemType.BootGear);
                    break;

                default:
                    Inventory.Instance.AddItemToInventoryListOnly(mouseItemData);
                    AddItemToSlot(mouseItemData);
                    break;
            }

            mouseSlot.RemoveItemFromMouse();
            return;
        }
        else if (myItemData != null && mouseItemData != null)
        {
            // TODO: Turn this into a "swap" function.
            Debug.Log("Cannot move item. Both slots are full.");
        }
        else
            Debug.Log("Cannot move item. Both slots are empty.");
    }

    void TryAddItemToGearSlot(ItemType type)
    {
        if (mouseSlot.MouseData.ItemType == type)
        {
            Debug.Log("This gear slot not setup yet.");

            // Replace this line with AddToGearList function
            Inventory.Instance.TryAddItemToInventory(mouseSlot.MouseData);
            return;
        }
        else
            Debug.Log("The item doesn't belong in this slot");
            Inventory.Instance.TryAddItemToInventory(mouseSlot.MouseData);
    }

    void ClearSlotData() 
    {
        myItemData = null;
        image.sprite = null;
        isEmpty = true;
    }
}

enum SlotType
{
    Inventory,
    Potion,
    MainHand,
    OffHand,
    Head,
    Chest,
    Boots
}
