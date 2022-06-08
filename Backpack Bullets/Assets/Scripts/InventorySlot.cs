using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class InventorySlot : MonoBehaviour
{
    [SerializeField] ItemBase myItemData;
    [SerializeField] SlotType slotType;
    [SerializeField] Image image;

    MouseSlot mouseSlot;
    bool isEmpty = true;
    Color fullColor = new Color(1, 1, 1, 1);
    Color transparent = new Color(0, 0, 0, 0);

    public bool IsEmpty => isEmpty;
    public ItemBase MyItemData => myItemData;


    private void Awake()
    {
        mouseSlot = FindObjectOfType<MouseSlot>();
    }

    private void Update()
    {
        if (image.sprite != null)
            image.color = fullColor;
        else
            image.color = transparent;

    }

    public void AddItemToSlot(ItemBase item)
    {
        if (item == null) return;

        myItemData = item;
        image.sprite = item.MyData.Icon;
        isEmpty = false;
    }

    public void RemoveItemFromSlot()
    {
        if (slotType == SlotType.Potion)
        {
            PotionBar.Instance.TryRemoveItemFromPotionBar(myItemData);
            ClearSlotData();
        }
        else if(slotType == SlotType.Inventory)
        {
            InventorySystem.Instance.TryRemoveItemFromInventory(myItemData);
            ClearSlotData();
        }
        else
        {
            GearSystem.Instance.TryRemoveItemFromGear(MyItemData);
            ClearSlotData();
        }

    }

    public void SlotClicked()
    {
        if (!UIManager.Instance.IsInventoryOpen) return;

        ItemBase mouseItemData = mouseSlot.MouseData;

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
                        InventorySystem.Instance.TryAddItemToInventory(mouseItemData);
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
                    TryAddItemToGearSlot(ItemType.FootGear);
                    break;

                default:
                    InventorySystem.Instance.AddItemToInventoryListOnly(mouseItemData);
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
        if (mouseSlot.MouseData.ItemType == type && myItemData == null)
        {
            if(GearSystem.Instance.TryAddItemToGear(mouseSlot.MouseData))
                AddItemToSlot(mouseSlot.MouseData);
            return;
        }
        else
            Debug.Log("The item doesn't belong in this slot");
        InventorySystem.Instance.TryAddItemToInventory(mouseSlot.MouseData);
    }

    void ClearSlotData() 
    {
        myItemData = null;
        image.sprite = null;
        isEmpty = true;
    }
}

public enum SlotType
{
    Inventory,
    Potion,
    MainHand,
    OffHand,
    Head,
    Chest,
    Boots
}
