using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] InventoryItemData myItemData;
    [SerializeField] TMP_Text stackText;
    Image image;
    string itemName;
    int maxStackSize;
    bool isEmpty = true;

    public int currentStackSize = 0;
    public bool IsEmpty => isEmpty;

    public InventoryItemData MyItemData => myItemData;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(myItemData == null)
        {
            stackText.enabled = false;
            return;
        }

        if(currentStackSize > 1)
        {
            stackText.enabled = true;
            stackText.text = currentStackSize.ToString();
        }
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
            maxStackSize = item.MaxStackSize;
            currentStackSize++;
            isEmpty = false;
        }
    }

    public void RemoveItemFromSlot()
    {
        if (Inventory.Instance.TryRemoveItemFromInventory(myItemData))
        {
            myItemData = null;
            image.sprite = null;
            itemName = null;
            maxStackSize = 0;
            currentStackSize = 0;
            isEmpty = true;
        }
    }
}
