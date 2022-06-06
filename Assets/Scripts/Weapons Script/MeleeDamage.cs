using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    
    [SerializeField] LayerMask collisionLayer;
    float radius = 2f;


    private void FixedUpdate()
    {
        var target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
        if (target)
        {

            if (target.tag == TagManager.ZOMBIE_HEALTH_TAG)
            {
                target.transform.root.GetComponent<ZombieController>().ZombieHurt(this.GetComponentInParent<MeleeController>().defaultConfig.damage);
            }

        }

    }
    

}
