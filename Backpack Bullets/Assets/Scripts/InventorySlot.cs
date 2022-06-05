using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] InventoryItemData myItemData;
    Image image;
    bool isEmpty = true;

    public bool IsEmpty => isEmpty;

    public InventoryItemData MyItemData => myItemData;

    private void Awake()
    {
        image = GetComponent<Image>();
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
        if (myItemData.ItemType == ItemType.Potion)
        {
            if (PotionBar.Instance.TryRemoveItemFromPotionBar(myItemData))
            {
                myItemData = null;
                image.sprite = null;
                isEmpty = true;
            }
        }
    }
}
