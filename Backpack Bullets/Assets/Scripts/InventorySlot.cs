using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] InventoryItemData myItemData;
    Image image;
    string itemName;
    bool isEmpty = true;

    public bool IsEmpty => isEmpty;

    public InventoryItemData MyItemData => myItemData;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnMouseDown()
    {
        if (myItemData != null)
        {
            RemoveItemFromSlot();
        }
    }

    public void AddItemToSlot(InventoryItemData item)
    {
        if(item != null)
        {
            myItemData = item;
            image.sprite = item.Icon;
            itemName = item.ItemName;
            isEmpty = false;
        }
    }

    public void RemoveItemFromSlot()
    {
        if (myItemData.ItemType == ItemType.Potion)
        {
            Debug.Log("Potion");
            if (PotionBar.Instance.TryRemoveItemFromPotionBar(myItemData))
            {
                myItemData = null;
                image.sprite = null;
                itemName = null;
                isEmpty = true;
            }
        }
    }
}
