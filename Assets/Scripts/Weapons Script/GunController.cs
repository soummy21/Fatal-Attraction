using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    [SerializeField] public Transform bulletPosition;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public ParticleSystem FX_Shot;
    [SerializeField] GameObject FX_BulletDrop;

    [SerializeField] AudioClip fire;
    
    
    //Refrences
    Collider2D fireCollider;

    
    WaitForSeconds fireCollider_waitTime = new WaitForSeconds(0.2f);

    private void Start()
    {
        if (!GameplayController.instance.bulletPoolCreated)  //Create POOL only once
        {
            GameplayController.instance.bulletPoolCreated = true;
            if (weaponNames != WeaponNames.Rocket && weaponNames != WeaponNames.FireGun)
            {
                
                SmartPool.instance.CreateBulletAndBulletFall(bulletPrefab, FX_BulletDrop, 30);
                
            }

        }else if(!GameplayController.instance.rocketPoolCreated)
        {
            
            if(weaponNames == WeaponNames.Rocket)
            {

                GameplayController.instance.rocketPoolCreated = true;
                SmartPool.instance.CreateRocket(bulletPrefab, 10);

            }
                
            
        }
        
        if(weaponNames==WeaponNames.FireGun)
        {
            fireCollider = bulletPosition.GetComponent<BoxCollider2D>();
        }

    }

    public override void ProcessAttack()
    {


       
        

        //Spawn Bullet
        if(transform!=null && weaponNames !=WeaponNames.FireGun) //Setting the the instantiations to active
        {
            if(weaponNames != WeaponNames.Rocket)
            {
                var bulletFallInstance = SmartPool.instance.SpawnBulletFall(bulletPosition.position, Quaternion.identity);
                bulletFallInstance.transform.localScale = new Vector3 (-Mathf.Sign(transform.root.localScale.x),1f,1f); //Set bullet fall direction
                StartCoroutine(ShootEffect());

            }

            
            SmartPool.instance.SpawnBullets(bulletPosition.position,-transform.root.localScale.x, bulletPosition.root.rotation, weaponNames, this, true);
           

            
        }
        else
        {
            StartCoroutine(ActivateBulletCollider());
        }

        
        

    }

    IEnumerator ShootEffect() //Shoot FX
    {
        yield return new WaitForSeconds(0.05f);
        FX_Shot.Play(); //For All Weapons

    }

    IEnumerator ActivateBulletCollider()
    {
        AudioSource.PlayClipAtPoint(fire, Camera.main.transform.position, 0.6f);
        FX_Shot.Play(); //For Fire Gun
        yield return new WaitForSeconds(0.2f);
        fireCollider.enabled = true;
        yield return fireCollider_waitTime;
        fireCollider.enabled = false;

        
    }

    


}
