using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class BulletController : MonoBehaviour
{
    int damage;
    

    [HideInInspector]
    public bool isPlayer;

    private float speed = 60f;

    IEnumerator CoroutineDeactivate; //Used so that we can use StopCoroutine()

    WaitForSeconds wait_Time = new WaitForSeconds(2f);

    float direction;
    [SerializeField] GameObject missileFX;


    


    void Start()
    {


        if (this.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            speed = 8f;
            wait_Time = new WaitForSeconds(3f);
        }


    }


    void Update()
    {
        transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0f, 0f));
        

    }

    private void OnEnable()
    {
        
       
        CoroutineDeactivate = WaitThenDeactive();
        StartCoroutine(CoroutineDeactivate);
    }

    private void OnDisable()
    {
        if (CoroutineDeactivate != null)
        {
            StopCoroutine(CoroutineDeactivate);
        }
    }

    IEnumerator WaitThenDeactive() //Reusable Bullets
    {
        yield return wait_Time;
        gameObject.SetActive(false);
    }


    public void SetDirection(float direction)
    {
        this.direction = direction;
    }

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }



    public void PlayMissileFX()
    {
        if (!missileFX)
        {
            return;
        }

        Instantiate(missileFX, transform.position, Quaternion.identity);
    }

    public void SetBulletTag(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

}
