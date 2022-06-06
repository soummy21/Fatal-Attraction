using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Class Declarations
    //Inspector Refrences
    [SerializeField]
    [Tooltip("Speed of Character")]
    float moveSpeed = 5f;    //Speed of Character



    //Class Refrences
    Rigidbody2D myRigidBody2D;
    PlayerAnimations playerAnimations;

    #endregion 
    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    
    
    private void FixedUpdate()
    {
        Move();              
    }

   void Move()
    {
        if (!GameplayController.instance.playerDead)
        {
            //Player Input
            var delX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            var delY = Input.GetAxisRaw("Vertical") * moveSpeed;
            //Moving Player using Velocity
            myRigidBody2D.velocity = new Vector2(delX, myRigidBody2D.velocity.y);
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, delY);
            //Checking if player moves
            bool isMovingHorizontal = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
            bool isMoving = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
            //Player Animations
            playerAnimations.PlayerMove(isMoving);
            //FlipSprite
            if (isMovingHorizontal)
            {
                transform.localScale = new Vector2((-Mathf.Sign(myRigidBody2D.velocity.x)) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

        }

        

    }

    
}
