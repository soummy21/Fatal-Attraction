using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //Class Refrences
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    //All Player Animation Setter Functions
    #region Animation Functions
    public void PlayerMove (bool run)
    {
        animator.SetBool(TagManager.RUN_PARAMETER,run);       
    }

    public void PlayerAttack()
    {
        animator.SetTrigger(TagManager.ATTACK_PARAMETER);
    }


    public void PlayerHurt()
    {
        animator.SetTrigger(TagManager.GET_HURT_PARAMETER);
    }
    
    public void PlayerDead()
    {
        animator.SetTrigger(TagManager.DEAD_PARAMETER);
    }

    public void SwitchWeaponAnimation(int typeWeapon)
    {
        animator.SetInteger(TagManager.TYPE_WEAPON_PARAMETER, typeWeapon);
        animator.SetTrigger(TagManager.SWITH_PARAMETER);
    }

    #endregion
    

}
