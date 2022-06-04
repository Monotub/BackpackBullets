using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Create Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] Sprite icon;
    [SerializeField] string itemName;
    [SerializeField] int maxStackSize;

    public int Id => id;
    public int MaxStackSize => maxStackSize;
    
    public Sprite Icon => icon;
    public string ItemName => itemName;

    

}

