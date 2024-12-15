using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    
    [SerializeField] LayerMask collisionLayer;
    float radius = 2f;

    private MeleeController meleeController;
    private ZombieController zombieController;

    private void Awake()
    {
        meleeController = GetComponentInParent<MeleeController>();
        zombieController = target.transform.root.GetComponent<ZombieController>()
    }

    private void FixedUpdate()
    {
        var target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
        if (target)
        {

            if (target.tag == TagManager.ZOMBIE_HEALTH_TAG)
            {
                zombieController.ZombieHurt(meleeController.defaultConfig.damage);
            }

        }

    }
    

}
