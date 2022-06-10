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
            PotionBar.Instance.TryRemoveFromPotionBar(myItemData);
            ClearSlotData();
        }
        else if(slotType == SlotType.Inventory)
        {
            InventorySystem.Instance.TryRemoveFromInventory(myItemData);
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
                        PotionBar.Instance.AddPotionToListOnly(mouseItemData);
                        AddItemToSlot(mouseItemData);
                    }
                    else
                        InventorySystem.Instance.TryAddToFirstAvailableSlot(mouseItemData);
                    break;

                case SlotType.MainHand:
                    TryAddItemToGearSlot(ItemType.MainHand);
                    break;

                case SlotType.OffHand:
                    TryAddItemToGearSlot(ItemType.OffHand);
                    break;

                case SlotType.HeadGear:
                    TryAddItemToGearSlot(ItemType.HeadGear);
                    break;

                case SlotType.ChestGear:
                    TryAddItemToGearSlot(ItemType.ChestGear);
                    break;

                case SlotType.FootGear:
                    TryAddItemToGearSlot(ItemType.FootGear);
                    break;

                default:
                    InventorySystem.Instance.AddItemToListOnly(mouseItemData);
                    AddItemToSlot(mouseItemData);
                    break;
            }

            mouseSlot.RemoveItemFromMouse();
            return;
        }
        else if (myItemData != null && mouseItemData != null)
        {
            // Item Swap functionality

            if (slotType == SlotType.Inventory)
            {
                SwapItemPrep(mouseItemData);
                InventorySystem.Instance.AddItemToCurrentSlot(this, mouseItemData);
            }
            else if (slotType == SlotType.Potion)
            {
                if (mouseItemData.ItemType == ItemType.Potion)
                {
                    SwapItemPrep(mouseItemData);
                    PotionBar.Instance.AddPotionToCurrentSlot(this, mouseItemData);
                }
            }
            else
            {
                switch (slotType)
                {
                    case SlotType.MainHand:
                        if(mouseItemData.ItemType == ItemType.MainHand)
                        {
                            SwapItemPrep(mouseItemData);
                            GearSystem.Instance.TryAddItemToGear(mouseItemData);
                            AddItemToSlot(mouseItemData);
                        }
                        break;

                    case SlotType.OffHand:
                        if (mouseItemData.ItemType == ItemType.OffHand)
                        {
                            SwapItemPrep(mouseItemData);
                            GearSystem.Instance.TryAddItemToGear(mouseItemData);
                            AddItemToSlot(mouseItemData);
                        }
                        break;

                    case SlotType.HeadGear:
                        if (mouseItemData.ItemType == ItemType.HeadGear)
                        {
                            SwapItemPrep(mouseItemData);
                            GearSystem.Instance.TryAddItemToGear(mouseItemData);
                            AddItemToSlot(mouseItemData);
                        }
                        break;

                    case SlotType.ChestGear:
                        if (mouseItemData.ItemType == ItemType.ChestGear)
                        {
                            SwapItemPrep(mouseItemData);
                            GearSystem.Instance.TryAddItemToGear(mouseItemData);
                            AddItemToSlot(mouseItemData);
                        }
                        break;

                    case SlotType.FootGear:
                        if (mouseItemData.ItemType == ItemType.FootGear)
                        {
                            SwapItemPrep(mouseItemData);
                            GearSystem.Instance.TryAddItemToGear(mouseItemData);
                            AddItemToSlot(mouseItemData);
                        }
                        break;

                    default:
                        Debug.LogWarning("Gear swapping not implemented yet");
                        break;

                }
                    
            }
        }
    }

    private void SwapItemPrep(ItemBase mouseItemData)
    {
        ItemBase tempSlotItem = myItemData;

        RemoveItemFromSlot();
        mouseSlot.RemoveItemFromMouse();

        mouseSlot.AddItemToMouse(tempSlotItem);
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
        InventorySystem.Instance.TryAddToFirstAvailableSlot(mouseSlot.MouseData);
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
    HeadGear,
    ChestGear,
    FootGear
}
