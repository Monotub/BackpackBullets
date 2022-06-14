using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] 

    
    GearSystem gear;
    StatSystem stats;
    PlayerControls controls;
    ParticleSystem.EmissionModule pEmission;

    private void Start()
    {
        gear = GearSystem.Instance;
        stats = StatSystem.Instance;
        controls = GetComponent<PlayerControls>();
        pEmission = particles.emission;
        pEmission.enabled = false;
    }

    private void OnEnable()
    {
        PlayerControls.OnPrimaryAttack += ProcessMainAttack;
        PlayerControls.OnSecondaryAttack += ProcessSecondaryAttack;
    }

    private void OnDisable()
    {
        PlayerControls.OnPrimaryAttack -= ProcessMainAttack;
        PlayerControls.OnSecondaryAttack -= ProcessSecondaryAttack;
    }


    private void ProcessMainAttack()
    {
        if (gear.mainHandGear == null)
        {
            Debug.Log("No item equipped in main hand");
        }
        else
        {
            switch (gear.mainHandGear.AttackType)
            {
                case AttackType.StatStick:
                    Debug.Log("Main Stat Sticks have no attack.");
                    break;

                case AttackType.Melee:
                    MeleeAttack();
                    break;
                case AttackType.Column:
                    ColumnAttack();
                    break;

                case AttackType.Cone:
                    ConeAttack();
                    break;


                default:
                    break;
            }
            
            pEmission.enabled = true;
        }
    }

    private void ProcessSecondaryAttack()
    {
        Debug.LogWarning("Secondary attacks not implemented yet.");
    }

    public void StopAllAttacks()
    {
        pEmission.enabled = false;
    }

    private void MeleeAttack()
    {
        var sh = particles.shape;
        sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
        
    }

    private void ColumnAttack()
    {
        var sh = particles.shape;
        sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
    }

    private void ConeAttack()
    {
        var sh = particles.shape;
        sh.shapeType = ParticleSystemShapeType.Cone;
    }
}
