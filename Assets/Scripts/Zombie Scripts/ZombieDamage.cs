using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    [SerializeField] float damage = 50;
    [SerializeField] LayerMask collisionLayer;
    float radius = 0.7f;
    void FixedUpdate()
    {
       if(LevelData.instance.zombieGoal == ZombieGoal.fence)
        {
            AttackFence();

        }else if(LevelData.instance.zombieGoal == ZombieGoal.player)
        {
            AttackPlayer();
        }
        
    }

    private void AttackPlayer()
    {
        var target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
        if (!GameplayController.instance.playerDead)
        {
            if (target)
            {
                if (target.tag == TagManager.PLAYER_HEALTH_TAG)
                {
                    target.GetComponent<PlayerHealth>().DamagePlayer(damage);
                }
            }
        }
    }

    void AttackFence()
    {
        var targetFence = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
        if (!GameplayController.instance.fenceBroken)
        {
            if (targetFence)
            {
                if (targetFence.tag == TagManager.FENCE_TAG)
                {
                    targetFence.GetComponent<FenceHealth>().DamageFence(damage);
                }
            }
        }
    }

}
