using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatSystem : MonoBehaviour
{
    public static StatSystem Instance { get; private set; }

    [SerializeField] TMP_Text strText;
    [SerializeField] TMP_Text intText;
    [SerializeField] TMP_Text dmgText;
    [SerializeField] TMP_Text defText;
    [SerializeField] TMP_Text speedText;

    float strBase = 10;
    float intBase = 10;
    float dmgBase = 10;
    float defBase = 10;
    float speedBase = 10;

    float strength;
    float intelligence;
    float damage;
    float defense;
    float moveSpeed;


    void Awake() { if (Instance == null) Instance = this; }

    private void Start()
    {
        strength = strBase;
        intelligence = intBase;
        damage = dmgBase;
        defense = defBase;
        moveSpeed = speedBase;
        
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

    public void UpdateStatValue(ItemType type, float value)
    {

        switch (type) 
        {
            case ItemType.MainHand:
                damage = dmgBase + value;
                UpdateStatUI();
                break;
        }


    }
}
