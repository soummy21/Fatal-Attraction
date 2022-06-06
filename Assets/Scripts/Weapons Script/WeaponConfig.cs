using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set Weapon Configuration
public enum TypeOfShooting { click,hold } 
public enum WeaponHoldType { melee,oneHand,twoHand } //Used for changing sprite and animating the character accordingly

[System.Serializable] //To show in inspector
public struct DefaultConfig
{
    public TypeOfShooting typeOfShot; 
    public WeaponHoldType weaponType;

    [Range(0, 100)]
    public int damage;

    [Range(0, 100)]
    public int critDamage;

    [Range(0.01f, 1.0f)]
    public float fireRate; 

    [Range(0, 100)]
    public int critRate;
}

