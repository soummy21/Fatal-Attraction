using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ZombieController : GunController
{
    
    ZombieAnimations zombieAnimations;
    ZombieMovement zombieMovement;

    private Transform targetTransform;

    bool isAlive = true;
    bool canAttack;

    [Header("Inherited Class Properties")]
    [SerializeField] float zombieHealth = 100;
    float initialHealth;

    [SerializeField] Transform zombieHealthBar;
    [SerializeField] GameObject zombieHealthObject;
    int fireDamage = 20;
    float tempScale=2f;

    [SerializeField] GameObject damagePoint;
    [SerializeField] Collider2D[] zombieColliders;

    [SerializeField] GameObject[] FX_DeadZombie;

    [SerializeField] float shootingRate = 2f;
    float initialRate;
    
    bool canShoot = true;

    [SerializeField]
    GameObject coin;
    [SerializeField]
    int noOfCoins;
    [SerializeField]
    int coinProbability;

    
    [SerializeField] AudioClip blast, rise, bloodSplatter, zombieAttack;
    
    void Start()
    {
        initialHealth = zombieHealth;
        zombieAnimations = GetComponent<ZombieAnimations>();
        zombieMovement = GetComponent<ZombieMovement>();
        UpdateHealthBar();
        AudioSource.PlayClipAtPoint(rise, Camera.main.transform.position, 0.65f);

        if (LevelData.instance.zombieGoal == ZombieGoal.player) 
        {
            targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        }else if(LevelData.instance.zombieGoal == ZombieGoal.fence)
        {
            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);
            targetTransform = fences[Random.Range(0, fences.Length)].transform;
        }

        initialRate = shootingRate;

        if (!GameplayController.instance.rocketPoolCreated)
        {

            if (weaponNames == WeaponNames.Rocket)
            {

                GameplayController.instance.rocketPoolCreated = true;
                SmartPool.instance.CreateRocket(bulletPrefab, 10);

            }


        }

    }

    
    void Update()
    {
        if(isAlive)
        {
            CheckDistance();
            zombieMovement.FlipSprite(targetTransform);
        }

        if(LevelData.instance.gameGoals[LevelData.instance.currentLevel-1] == GameGoal.Game_Over)
        {
            Destroy(gameObject);
        }
    }

   
    private void CheckDistance()
    {
        if(targetTransform)
        {
            if(Vector2.Distance(targetTransform.position,transform.position)>1.5f)
            {

                
                zombieMovement.Move(targetTransform);
                if(bulletPosition != null)
                {
                    ShootPlayer();
                    canShoot = true;
                }
                
            }
            else
            {
                if(canAttack)
                {
                    canShoot = false;
                    zombieAnimations.Attack();
                }
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG  || target.tag == TagManager.FENCE_TAG)
        { 
            canAttack = true;
        }

        if(target.tag== TagManager.BULLET_TAG || target.tag== TagManager.ROCKET_MISSILE_TAG)
        {
            if (target.gameObject.GetComponent<BulletController>().isPlayer)
            {
                UpdateHealth(target);
            }


            if (target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.GetComponent<BulletController>().PlayMissileFX();
                AudioSource.PlayClipAtPoint(blast, Camera.main.transform.position, 0.9f);
            }

            if(zombieHealth<=0)
            {
                AudioSource.PlayClipAtPoint(bloodSplatter, Camera.main.transform.position, 1f);
                InstantiateCoin();
                Destroy(zombieHealthObject);
                isAlive = false;
                zombieAnimations.Dead();
                foreach (Collider2D collider in zombieColliders)
                {
                    collider.enabled = false;
                }
                StartCoroutine(DeactivateZombie());
            }
            if (target.gameObject.GetComponent<BulletController>().isPlayer)
            {

                target.gameObject.SetActive(false);
            }
        }

        if(target.tag == TagManager.FIRE_BULLET_TAG)
        {
            ZombieHurt(fireDamage);
        }
    }


    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = false;
        }
    }

    #region ZombieHealth
    void UpdateHealthBar()
    {
        tempScale = zombieHealth / initialHealth;
        zombieHealthBar.localScale = new Vector2(tempScale, zombieHealthBar.localScale.y);
    }
    private void UpdateHealth(Collider2D target)
    {
        zombieAnimations.Hurt();
       
        zombieHealth -= target.GetComponent<BulletController>().Damage;
        UpdateHealthBar();
    }

    public void ZombieHurt(int damage)
    {
        zombieAnimations.Hurt();
        
        zombieHealth -= damage;
        UpdateHealthBar();

        if (zombieHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(bloodSplatter, Camera.main.transform.position, 1f);
            InstantiateCoin();
            Destroy(zombieHealthObject);
            isAlive = false;
            foreach (Collider2D collider in zombieColliders)
            {
                collider.enabled = false;
            }

            zombieAnimations.Dead();
            StartCoroutine(DeactivateZombie());
        }
    }

    
    IEnumerator DeactivateZombie()
    {
        GameplayController.instance.ZombieUpdater();
        yield return new WaitForSeconds(2f);
        InstantiateCoin();
        gameObject.SetActive(false);
    }

    #endregion   

    public void ActivateDeadFX(int index)
    {
        FX_DeadZombie[index].SetActive(true);
        if(FX_DeadZombie[index].GetComponent<ParticleSystem>())
        {
            FX_DeadZombie[index].GetComponent<ParticleSystem>().Play();
        }
    }


    #region ZombieMelee
    void ActivateDamagePoint()
    {
        AudioSource.PlayClipAtPoint(zombieAttack, Camera.main.transform.position, 1f);
        damagePoint.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }

    #endregion

    #region ZombieProjectile

    void ShootPlayer()
    {
        shootingRate -= Time.deltaTime;
        if(shootingRate<=0)
        {
            if (canShoot)
            {
                zombieAnimations.Shoot();
                

            }
            shootingRate = initialRate;
        }
        
    }


    public void ActivateBullet()
    {

        SmartPool.instance.SpawnBullets(bulletPosition.position, -transform.root.localScale.x, bulletPosition.root.rotation, weaponNames, this, false);
        if(FX_Shot !=null)
        {
            FX_Shot.Play();
            FX_Shot.gameObject.transform.localScale = new Vector2(Mathf.Sign(transform.root.localScale.x), 1f);
        }
        
    }

    

    #endregion

    void InstantiateCoin()
    {
        if(Random.Range(0,6)<coinProbability)
        {
            for (int i = 0; i < noOfCoins; i++)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
        }
    }

}



