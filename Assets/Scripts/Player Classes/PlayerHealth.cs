using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject[] blood_FX;
    PlayerAnimations playerAnimations;
    [SerializeField] float health = 100;
    [SerializeField] AudioClip hurt, lossSound, blast1;

    Collider2D healthCollider;

   
    private void Awake()
    {
        
        playerAnimations = GetComponentInParent<PlayerAnimations>();
        healthCollider = GetComponent<Collider2D>();
    }

    public void DamagePlayer(float damage)
    {

        health -= damage;
        AudioSource.PlayClipAtPoint(hurt, Camera.main.transform.position, 0.8f);
        playerAnimations.PlayerHurt();
        GameplayController.instance.PlayerHealthBar(damage);

        if(health<=0)
        {
            
            Time.timeScale = 0.3f;
            AudioSource.PlayClipAtPoint(lossSound, Camera.main.transform.position, 1f);
            GameplayController.instance.failMessage.SetActive(true);
            GameplayController.instance.playerDead = true;
            healthCollider.enabled = false;
            playerAnimations.PlayerDead();
            blood_FX[Random.Range(0, blood_FX.Length)].GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == TagManager.BULLET_TAG || collision.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            if (!collision.gameObject.GetComponent<BulletController>().isPlayer)
            {
                DamagePlayer(collision.GetComponent<BulletController>().Damage);
            }

            if(collision.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                AudioSource.PlayClipAtPoint(blast1, Camera.main.transform.position, 0.9f);
                collision.gameObject.GetComponent<BulletController>().PlayMissileFX();
                collision.gameObject.SetActive(false);
            }
        }
    }

}
