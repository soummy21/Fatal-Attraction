using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour
{
    [Header("Level Type")]
    public GameGoal[] gameGoals;

    [HideInInspector]
    public ZombieGoal zombieGoal;

    public int currentLevel = 0;

    
    public int[] levelPara;
   



    [SerializeField] public string[] successMessage;
    [SerializeField] public string[] gameStartMessage;
    [SerializeField] public string[] failMessage;
    [SerializeField] public string[] missionText;

    public Sprite[] gameGoalImages;

    public static LevelData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   

}
