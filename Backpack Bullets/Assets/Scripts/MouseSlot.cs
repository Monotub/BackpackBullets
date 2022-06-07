using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSlot : MonoBehaviour
{
    [SerializeField] Image mouseImage;
    [SerializeField] InventoryItemData mouseData = null;

    public InventoryItemData MouseData => mouseData;


    private void Start()
    {
        mouseImage.enabled = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        if (mouseData != null) mouseImage.enabled = true;
        else mouseImage.enabled = false;
    }

    public void AddItemToMouse(InventoryItemData item)
    {
        if(mouseData == null)
        {
            mouseData = item;
            mouseImage.sprite = item.Icon;
        }
    }

    public void RemoveItemFromMouse()
    {
        mouseData = null;
        mouseImage.sprite = null;
    }
}
