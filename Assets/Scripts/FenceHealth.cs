using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceHealth : MonoBehaviour
{
    [SerializeField] float fenceHealth = 100;
    [SerializeField] GameObject FX_Hurt;
    [SerializeField] GameObject FX_Dead;

    public void DamageFence(float damage)
    {
        fenceHealth -= damage;
        FX_Hurt.GetComponent<ParticleSystem>().Play();

        if(fenceHealth<=0)
        {
            FX_Dead.GetComponent<ParticleSystem>().Play();
            GameplayController.instance.fenceBroken = true;
            GameplayController.instance.failMessage.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DisableFence());
            
        }
    }

    IEnumerator DisableFence()
    {
        yield return new WaitForSeconds(0.35f);
        gameObject.SetActive(false);
    }
}
