using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponNames { Bat, MP5, AK47, Rocket, AWP, FireGun, M3, Pistol }

public class WeaponController : MonoBehaviour
{
    public DefaultConfig defaultConfig;
    public WeaponNames weaponNames;

    protected PlayerAnimations playerAnimations;
    protected float lastShotTime;

    [SerializeField] int gunIndex;
    [HideInInspector]
    int currentAmmo;
    [SerializeField] int maxAmmo;

    [SerializeField] AudioClip outOfAmmo;

    private void Awake()
    {
        playerAnimations = GetComponentInParent<PlayerAnimations>();
        currentAmmo = maxAmmo;
    }

    
    public void CallAttack() //Used to Check Fire Rate and Ammo
    {
        if(Time.time > lastShotTime + defaultConfig.fireRate )
        {
            if(currentAmmo>0)
            {
                ProcessAttack();
                playerAnimations.PlayerAttack();
                lastShotTime = Time.time;
                currentAmmo--;
            }
            else
            {
                AudioSource.PlayClipAtPoint(outOfAmmo, Camera.main.transform.position, 0.6f);
            }
        }
    }

    public virtual void ProcessAttack() //for the actual attack
    {

    }
}
