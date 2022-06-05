using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Create Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] ItemType itemType;
    [SerializeField] string itemName;
    [SerializeField] Sprite icon;

    public Sprite Icon => icon;
    public string ItemName => itemName;
    public ItemType ItemType => itemType;
}

public enum ItemType
{
    Potion,
    Weapon,
    Armor
}

