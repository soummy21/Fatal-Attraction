using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField]
    float zombieSpeed = 1f;
    public void Move(Transform target)
    {
        
        FlipSprite(target);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, target.transform.position.y - 0.93f),zombieSpeed*Time.deltaTime);
        
    }

    public void FlipSprite(Transform target)
    {
        
        if(transform.position.x - 0.08f > target.position.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }else if(transform.position.x + 0.08f < target.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    } 
}
