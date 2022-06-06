using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To change the sprite from one hand to two hand and vice versa
public class PlayerArmController : MonoBehaviour
{
    [SerializeField] Sprite oneHand, twoHand;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    public void ChangeToOneHand()
    {
        spriteRenderer.sprite = oneHand;
    }

    public void ChangeToTwoHand()
    {
        spriteRenderer.sprite = twoHand;
    }
}
