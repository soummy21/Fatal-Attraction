using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;




public class SmartPool : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Transform zombieTransform;

    [SerializeField] AudioClip shotSound, rocketSound;


    public static SmartPool instance;

    //List to store the pool of objects
    private List<GameObject> bullet_prefabs = new List<GameObject>(); 
    private List<GameObject> bullet_fall_prefab = new List<GameObject>();
    private List<GameObject> rocket_bullet_prefab = new List<GameObject>();

   

    private void Awake()
    {
        MakeInstance();
    }

   

    private void OnDisable()
    {
        instance = null;
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    

    //Creating a Pool for count no of bullets,rockets and bullet falls
    public void CreateBulletAndBulletFall(GameObject bullet, GameObject bulletFall, int count)
    {

        for(int i=0; i<count; i++)
        {
            var tempBullet = Instantiate(bullet);
            var tempBulletFall = Instantiate(bulletFall);
            bullet_prefabs.Add(tempBullet);
            bullet_fall_prefab.Add(tempBulletFall);
            bullet_prefabs[i].SetActive(false);
            bullet_fall_prefab[i].SetActive(false);
        }
    }


    public void CreateRocket(GameObject rocket,int count)
    {
        for(int i=0; i<count;i++)
        {
            var tempRocket = Instantiate(rocket);
            rocket_bullet_prefab.Add(tempRocket);
            rocket_bullet_prefab[i].SetActive(false);
        }
    }


    //setting bulletfall from the pool active at position and rotation
    public GameObject SpawnBulletFall(Vector3 position , Quaternion rotation)
    {
        for(int i=0;i<bullet_fall_prefab.Count; i++)
        {
            if(!bullet_fall_prefab[i].activeInHierarchy)
            {
                bullet_fall_prefab[i].SetActive(true);
                bullet_fall_prefab[i].transform.position = position;
                bullet_fall_prefab[i].transform.rotation = rotation;
                return bullet_fall_prefab[i]; //returns and exits loop
            }
        }

        return null;
    }

    
    public void SpawnBullets(Vector3 position, float direction, Quaternion rotation, WeaponNames name, GunController weaponController, bool bulletType)
    {
        if (name != WeaponNames.Rocket)
        {
            for (int i = 0; i < bullet_prefabs.Count; i++)
            {
                if (!bullet_fall_prefab[i].activeInHierarchy)
                {
                    bullet_prefabs[i].SetActive(true);
                    bullet_prefabs[i].transform.position = position;
                    bullet_prefabs[i].transform.rotation = rotation;
                    bullet_prefabs[i].GetComponent<BulletController>().SetDirection(direction); // to set direction of the bullet
                    SetBulletDamage( bullet_prefabs[i], weaponController);
                    SetTag(bullet_prefabs[i], bulletType,name);
                    AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, 0.5f);
                    break; //returns and exits loop
                }
            }
        }
        else
        {
            for (int i = 0; i < rocket_bullet_prefab.Count; i++)
            {
                if (!rocket_bullet_prefab[i].activeInHierarchy)
                {

                    rocket_bullet_prefab[i].SetActive(true);
                    rocket_bullet_prefab[i].transform.position = position;
                    rocket_bullet_prefab[i].transform.rotation = rotation;
                    rocket_bullet_prefab[i].GetComponent<BulletController>().SetDirection(direction);
                    SetBulletDamage(rocket_bullet_prefab[i], weaponController);
                    SetTag(rocket_bullet_prefab[i], bulletType,name);
                    AudioSource.PlayClipAtPoint(rocketSound, Camera.main.transform.position, 0.7f);
                    break;

                }
            }



        }

    }

     public void SetBulletDamage(GameObject bullet,GunController weaponController)
     {
         bullet.GetComponent<BulletController>().Damage = weaponController.defaultConfig.damage;     
        
     }

    void SetTag(GameObject bullet, bool temp, WeaponNames weaponNames)
    {
        bullet.GetComponent<BulletController>().SetBulletTag(temp);

        if (weaponNames == WeaponNames.Rocket)
        {
            bullet.transform.localScale = temp ? playerTransform.localScale : zombieTransform.localScale;
        }

    }
  
    

}
