using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSystem : MonoBehaviour
{
    [SerializeField] MainHand mainHandGear;
    [SerializeField] OffHand offHandGear;
    [SerializeField] HeadGear headGear;
    [SerializeField] ChestGear chestGear;
    [SerializeField] FootGear footGear;

    public ItemBase MainHand => mainHandGear;

    public static GearSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool TryAddItemToGear(ItemBase item)
    {
        switch (item.ItemType)
        {
            case ItemType.MainHand:
                if (mainHandGear == null)
                {
                    mainHandGear = item.GetComponent<MainHand>();
                    StatSystem.Instance.UpdateStatValue(ItemType.MainHand, mainHandGear.WeaponDamage);
                    return true;
                }
                else
                    return false;

            case ItemType.OffHand:
                if (offHandGear == null)
                {
                    offHandGear = item.GetComponent<OffHand>();
                    return true;
                }
                else
                    return false;

            case ItemType.HeadGear:
                if (headGear == null)
                {
                    headGear = item.GetComponent<HeadGear>();
                    return true;
                }
                else
                    return false;

            case ItemType.ChestGear:
                if (chestGear == null)
                {
                    chestGear = item.GetComponent<ChestGear>();
                    return true;
                }
                else
                    return false;

            case ItemType.FootGear:
                if (footGear == null)
                {
                    footGear = item.GetComponent<FootGear>();
                    return true;
                }
                else
                    return false;

            default:
                return false;
        }
        
    }

    public bool TryRemoveItemFromGear(ItemBase item)
    {
        switch (item.ItemType)
        {
            case ItemType.MainHand:
                mainHandGear = null;
                StatSystem.Instance.UpdateStatValue(ItemType.MainHand, 0f);
                return true;

            case ItemType.OffHand:
                offHandGear = null;
                return true;

            case ItemType.HeadGear:
                headGear = null;
                return true;

            case ItemType.ChestGear:
                chestGear = null;
                return true;

            case ItemType.FootGear:
                footGear = null;
                return true;

            default:
                return false;
        }
    }
}
