using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainHand : GearBase
{
    [Header("Weapon Combat Properties")]
    [SerializeField] AttackType attackType;
    [SerializeField] float attackDelay = 0.5f;

    public AttackType AttackType => attackType;
    public float AttackDelay => attackDelay;


}
public enum AttackType
{
    StatStick,
    Melee,
    Column,
    Cone
}
