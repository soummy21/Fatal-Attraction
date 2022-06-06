using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    List<WeaponController> unlocked_weapon = new List<WeaponController>();
    [SerializeField] WeaponController[] lockedWeapons;
    [SerializeField] GameObject meleeDamagePoint;
    [SerializeField] AudioClip changeWeapon;

    private WeaponController currentWeapon;

    PlayerArmController[] playerArmController;
    PlayerAnimations playerAnimations;
    bool isShooting;

    int currentWeaponIndex;
    TypeOfShooting currentTypeOfShot;
    MainMenuController mainMenuController;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        
        LoadActiveWeapons();
        currentWeaponIndex = 1; //start with pistol
        mainMenuController = FindObjectOfType<MainMenuController>();
    }
    void Start()
    {
        playerArmController = GetComponentsInChildren<PlayerArmController>();
        playerAnimations.SwitchWeaponAnimation((int)unlocked_weapon[currentWeaponIndex].defaultConfig.weaponType); //intialize the weapon type index
        ChangeWeapon(unlocked_weapon[currentWeaponIndex]);
        

    }
    
   

    

    void LoadActiveWeapons() //Unlock Weapon System
    {
        if (PlayerPrefs.GetString(TagManager.UNLOCK_M3_KEY, "NONE") == "YES0")
        {
            unlocked_weapon.Add(lockedWeapons[0]);
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_AK_KEY, "NONE1") == "YES1")
        {
            unlocked_weapon.Add(lockedWeapons[1]);
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_FIRE_KEY, "NONE2") == "YES2")
        {
            unlocked_weapon.Add(lockedWeapons[2]);
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_SNIPER_KEY, "NONE3") == "YES3")
        {
            unlocked_weapon.Add(lockedWeapons[3]);
        }

         if (PlayerPrefs.GetString(TagManager.UNLOCK_ROCKET_KEY, "NONE4") == "YES4")
        {
            unlocked_weapon.Add(lockedWeapons[4]);
        }


       
    }

    public void SwitchWeapon() //Switching Weapon Syste.
    {
        currentWeaponIndex++;
        currentWeaponIndex = (currentWeaponIndex >= unlocked_weapon.Count) ? 0 : currentWeaponIndex; //Checks if the next weapon is out of index, if yes switches to first
        ChangeWeapon(unlocked_weapon[currentWeaponIndex]);
        AudioSource.PlayClipAtPoint(changeWeapon,Camera.main.transform.position, 0.45f);
        playerAnimations.SwitchWeaponAnimation((int)unlocked_weapon[currentWeaponIndex].defaultConfig.weaponType); //play switch animation according to the weapon type index
        
    }

    void ChangeWeapon(WeaponController newWeapon) //Used to Change Weapon
    {
        if (currentWeapon)
        { currentWeapon.gameObject.SetActive(false); }//disable current weapon

        currentWeapon = newWeapon; 
        currentTypeOfShot = newWeapon.defaultConfig.typeOfShot;

        currentWeapon.gameObject.SetActive(true); 

        if(newWeapon.defaultConfig.weaponType == WeaponHoldType.twoHand) //change sprite to one hand or two hand weapon accordingly
        {
            for( int i = 0 ; i< playerArmController.Length; i++)
            {
                playerArmController[i].ChangeToTwoHand();
            }
        }else
        {
            for (int i = 0; i < playerArmController.Length; i++)
            {
                playerArmController[i].ChangeToOneHand();
            }
        }


    }

    public void ShootWeapon() //Logic for continuous or one time shooting
    {
        currentTypeOfShot = currentWeapon.defaultConfig.typeOfShot;
        if(currentTypeOfShot==TypeOfShooting.hold)
        {
            currentWeapon.CallAttack();

        }else if(currentTypeOfShot == TypeOfShooting.click)
        {
            if(!isShooting)
            {
                currentWeapon.CallAttack();
                isShooting = true;
            }
        }
    }

   public void ResetAttack()
    {
        isShooting = false;
    }

    public void AllowCollisionDetection()
    {

        meleeDamagePoint.SetActive(true);
    }

    public void DenyCollisionDetection()
    {
       
        meleeDamagePoint.SetActive(false);
    }

}
