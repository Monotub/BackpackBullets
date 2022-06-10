using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatSystem : MonoBehaviour
{
    [Header("Stat Screen References")]
    [SerializeField] TMP_Text strText;
    [SerializeField] TMP_Text intText;
    [SerializeField] TMP_Text dmgText;
    [SerializeField] TMP_Text defText;
    [SerializeField] TMP_Text speedText;

    public static StatSystem Instance { get; private set; }

    GearSystem gearSystem;

    // Stat base values
    float strBase = 10;
    float intBase = 10;
    float dmgBase = 10;
    float defBase = 10;
    float moveSpeedBase = 10;

    // Final stat totals
    float strength;
    float intelligence;
    float damage;
    float defense;
    float moveSpeed;


    void Awake() 
    { 
        if (Instance == null) Instance = this;

        gearSystem = FindObjectOfType<GearSystem>();
    }

    private void Start()
    {
        strength = strBase;
        intelligence = intBase;
        damage = dmgBase;
        defense = defBase;
        moveSpeed = moveSpeedBase;
        
        UpdateStatUI();
    }

    void UpdateStatUI()
    {
        strText.text = strength.ToString("n0");
        intText.text = intelligence.ToString("n0");
        dmgText.text = damage.ToString("n0");
        defText.text = defense.ToString("n0");
        speedText.text = moveSpeed.ToString("n0");
    }

    public void UpdateStatValues()
    {
        // stat = basestat + main + off + helm + chest + boots

        // Strength
        float tempStr = 0;
        if (gearSystem.mainHandGear != null) tempStr += gearSystem.mainHandGear.Strength;
        if (gearSystem.offHandGear != null) tempStr += gearSystem.offHandGear.Strength;
        if (gearSystem.headGear != null) tempStr += gearSystem.headGear.Strength;
        if (gearSystem.chestGear != null) tempStr += gearSystem.chestGear.Strength;
        if (gearSystem.footGear != null) tempStr += gearSystem.footGear.Strength;
        strength = strBase + tempStr;

        // Intelligence
        float tempInt = 0;
        if (gearSystem.mainHandGear != null) tempInt += gearSystem.mainHandGear.Intelligence;
        if (gearSystem.offHandGear != null) tempInt += gearSystem.offHandGear.Intelligence;
        if (gearSystem.headGear != null) tempInt += gearSystem.headGear.Intelligence;
        if (gearSystem.chestGear != null) tempInt += gearSystem.chestGear.Intelligence;
        if (gearSystem.footGear != null) tempInt += gearSystem.footGear.Intelligence;
        intelligence = strBase + tempInt;

        // Damage
        float tempDmg = 0;
        if (gearSystem.mainHandGear != null) tempDmg += gearSystem.mainHandGear.Damage;
        if (gearSystem.offHandGear != null) tempDmg += gearSystem.offHandGear.Damage;
        if (gearSystem.headGear != null) tempDmg += gearSystem.headGear.Damage;
        if (gearSystem.chestGear != null) tempDmg += gearSystem.chestGear.Damage;
        if (gearSystem.footGear != null) tempDmg += gearSystem.footGear.Damage;
        damage = dmgBase + tempDmg;

        // Defense
        float tempDef = 0;
        if (gearSystem.mainHandGear != null) tempDef += gearSystem.mainHandGear.Defense;
        if (gearSystem.offHandGear != null) tempDef += gearSystem.offHandGear.Defense;
        if (gearSystem.headGear != null) tempDef += gearSystem.headGear.Defense;
        if (gearSystem.chestGear != null) tempDef += gearSystem.chestGear.Defense;
        if (gearSystem.footGear != null) tempDef += gearSystem.footGear.Defense;
        defense = defBase + tempDef;

        // Move Speed
        float tempMove = 0;
        if (gearSystem.mainHandGear != null) tempMove += gearSystem.mainHandGear.MoveSpeed;
        if (gearSystem.offHandGear != null) tempMove += gearSystem.offHandGear.MoveSpeed;
        if (gearSystem.headGear != null) tempMove += gearSystem.headGear.MoveSpeed;
        if (gearSystem.chestGear != null) tempMove += gearSystem.chestGear.MoveSpeed;
        if (gearSystem.footGear != null) tempMove += gearSystem.footGear.MoveSpeed;
        moveSpeed = moveSpeedBase + tempMove;

        UpdateStatUI();
    }
}
