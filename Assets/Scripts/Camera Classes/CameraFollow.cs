using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(LevelData.instance.gameGoals[LevelData.instance.currentLevel] != GameGoal.Protect_Fence )
        {
            if (playerTransform)
            {
                var tempPos = transform.position;
                tempPos.x = playerTransform.position.x;
                transform.position = tempPos;

            }
        }
        
        
    }
}
