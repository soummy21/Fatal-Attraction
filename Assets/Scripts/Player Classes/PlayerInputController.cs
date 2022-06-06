using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Input Manager For The Player
public class PlayerInputController : MonoBehaviour
{

    bool canShoot = true;
    WeaponManager weaponManager;
    
    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            weaponManager.SwitchWeapon();
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (canShoot) 
            {
                weaponManager.ShootWeapon();
            }
            
        }else
        {
            weaponManager.ResetAttack();
        }

        
    }
}
