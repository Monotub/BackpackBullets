using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSystem : MonoBehaviour
{
    public MainHand mainHandGear { get; private set; }
    public OffHand offHandGear { get; private set; }
    public HeadGear headGear { get; private set; }
    public ChestGear chestGear { get; private set; }
    public FootGear footGear { get; private set; }


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
                    StatSystem.Instance.UpdateStatValues();
                    return true;
                }
                else
                    return false;

            case ItemType.OffHand:
                if (offHandGear == null)
                {
                    offHandGear = item.GetComponent<OffHand>();
                    StatSystem.Instance.UpdateStatValues();
                    return true;
                }
                else
                    return false;

            case ItemType.HeadGear:
                if (headGear == null)
                {
                    headGear = item.GetComponent<HeadGear>();
                    StatSystem.Instance.UpdateStatValues();
                    return true;
                }
                else
                    return false;

            case ItemType.ChestGear:
                if (chestGear == null)
                {
                    chestGear = item.GetComponent<ChestGear>();
                    StatSystem.Instance.UpdateStatValues();
                    return true;
                }
                else
                    return false;

            case ItemType.FootGear:
                if (footGear == null)
                {
                    footGear = item.GetComponent<FootGear>();
                    StatSystem.Instance.UpdateStatValues();
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
                StatSystem.Instance.UpdateStatValues();
                return true;

            case ItemType.OffHand:
                offHandGear = null;
                StatSystem.Instance.UpdateStatValues();
                return true;

            case ItemType.HeadGear:
                headGear = null;
                StatSystem.Instance.UpdateStatValues();
                return true;

            case ItemType.ChestGear:
                chestGear = null;
                StatSystem.Instance.UpdateStatValues();
                return true;

            case ItemType.FootGear:
                footGear = null;
                StatSystem.Instance.UpdateStatValues();
                return true;

            default:
                return false;
        }
    }
}
