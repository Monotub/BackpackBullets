using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBase : ItemBase
{
    [SerializeField] float strength;
    [SerializeField] float intelligence;
    [SerializeField] float damage;
    [SerializeField] float defense;
    [SerializeField] float moveSpeed;

    public float Strength => strength;
    public float Intelligence => intelligence;
    public float Damage => damage;
    public float Defense => defense;
    public float MoveSpeed => moveSpeed;

}
