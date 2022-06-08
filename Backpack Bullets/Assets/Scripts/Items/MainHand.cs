using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainHand : ItemBase
{
    [Header("Weapon Stats")]
    [SerializeField] float weaponDamage;

    public float WeaponDamage => weaponDamage;
}
