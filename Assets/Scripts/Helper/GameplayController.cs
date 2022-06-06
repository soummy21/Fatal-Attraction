using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;
using UnityEngine.Timeline;

public enum ZombieGoal { player, fence };
public enum GameGoal { Kill_Zombies, Travel_Distance, Outrun_Timer, Protect_Fence, Game_Over}


public class GameplayController : MonoBehaviour
{
    MainMenuController mainMenuController;
    //For Level Parameters
    Transform playerTransform;
    Vector2 playerPreviousPos;
    float stepCount;
    float zombieCount;
    float timeCount;


    [Header("Gameplay UI")]
    [SerializeField] Image UI_Left;
    [SerializeField] TextMeshProUGUI UI_Text;
    [SerializeField] Image Player_Health;

    [Header("Messages/Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject successMessage;
    [SerializeField] GameObject gameStartMessage;
    [SerializeField]public GameObject failMessage;

    [Header("Panel Texts")]
    [SerializeField] TextMeshProUGUI [] missionText;
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] TextMeshProUGUI failureText;
    [SerializeField] TextMeshProUGUI startText;

    public static GameplayController instance;  // Static Object(No refrencing required)

    [HideInInspector] public bool bulletPoolCreated = false; //to create just a single pool of bullets
    [HideInInspector] public bool rocketPoolCreated = false; //to create just a single pool of rockets
    [HideInInspector] public bool playerDead = false;
    [HideInInspector] public bool fenceBroken = false;

    [SerializeField] AudioClip panel, winSound, buttons;

    int storePP;
    bool levelEnded = false;
    float times = 2f;

    //Level Data Ref
    int levelNo;

    private void Awake()
    {

        MakeInstance();
        levelNo = LevelData.instance.currentLevel - 1;
        SetTexts();
        if(levelNo == 6)
        {
            LevelData.instance.zombieGoal = ZombieGoal.fence;
        }else
        {
            LevelData.instance.zombieGoal = ZombieGoal.player;
        }
        AudioSource.PlayClipAtPoint(panel, Camera.main.transform.position, 0.75f);
        gameStartMessage.SetActive(true);
        Time.timeScale = 0.0f;
        storePP = PlayerPrefsController.instance.GetSceneIndex();
        
    }

    void SetTexts()
    {
        foreach(var mission in missionText)
        {
            mission.text = LevelData.instance.missionText[levelNo];
        }
        successText.text = LevelData.instance.successMessage[levelNo];
        failureText.text = LevelData.instance.failMessage[levelNo];
        startText.text = LevelData.instance.gameStartMessage[levelNo];
        UI_Left.sprite = LevelData.instance.gameGoalImages[levelNo];
    }

    private void Start()
    {

        if(LevelData.instance.gameGoals[levelNo] == GameGoal.Travel_Distance)
        {
            playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            playerPreviousPos = playerTransform.position;
            stepCount = LevelData.instance.levelPara[levelNo];
            UI_Text.text = stepCount.ToString();
            
        }

        if(LevelData.instance.gameGoals[levelNo] == GameGoal.Outrun_Timer)
        {
            timeCount = LevelData.instance.levelPara[levelNo];
            UI_Text.text = timeCount.ToString();
            InvokeRepeating("RunTimer", 1f, 1f);
        }

        if(LevelData.instance.gameGoals[levelNo] == GameGoal.Kill_Zombies)
        {
            zombieCount = LevelData.instance.levelPara[levelNo];
            UI_Text.text = zombieCount.ToString();
        }

        mainMenuController = FindObjectOfType<MainMenuController>();
    }

    private void Update()
    {
        if(LevelData.instance.gameGoals[levelNo] == GameGoal.Travel_Distance)
        {
            CountPlayerMovement();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1.0f;
            AudioSource.PlayClipAtPoint(panel, Camera.main.transform.position, 0.75f); ;
            gameStartMessage.SetActive(false);
        }

        if(levelEnded)
        {
            LevelEndTimer();
        }
    }

    private void CountPlayerMovement()
    {
        var playerCurrentPosition = playerTransform.position;
        double dist = playerCurrentPosition.x - playerPreviousPos.x;

        if (playerCurrentPosition.x > playerPreviousPos.x)
        {
            if (dist > 1)
            {
                stepCount--;
                if (stepCount <= 0)
                {
                    LevelComplete();
                }

                playerPreviousPos = playerCurrentPosition;
                UI_Text.text = stepCount.ToString() + "m";

            }
        }
        else if (playerCurrentPosition.x < playerPreviousPos.x)
        {
            if (dist > 0.8)
            {
                stepCount++;
                if (stepCount >= LevelData.instance.levelPara[levelNo])
                {
                    stepCount = LevelData.instance.levelPara[levelNo];

                    playerPreviousPos = playerCurrentPosition;
                    UI_Text.text = stepCount.ToString() + "m";
                }



            }

            





        }
    }
    private void OnDisable()
    {
        instance = null;
    }


    void MakeInstance()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        AudioSource.PlayClipAtPoint(buttons, Camera.main.transform.position, 1f);
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1.0f;
        AudioSource.PlayClipAtPoint(buttons,Camera.main.transform.position, 1f);
        pauseMenu.SetActive(false);
    }

    void RunTimer()
    {
        timeCount--;
        UI_Text.text = timeCount.ToString();

        if(timeCount<=0)
        {
            
            CancelInvoke("RunTimer");
            LevelComplete();
        }
    }

    void LevelEndTimer()
    {
        
        times -= Time.deltaTime;
        if(times<=0)
        {
            successMessage.gameObject.SetActive(true);
            Time.timeScale = 0.3f;

        }
    }

    public void ZombieUpdater()
    {
        if(LevelData.instance.gameGoals[levelNo] == GameGoal.Kill_Zombies)
        {
            zombieCount--;
            UI_Text.text = zombieCount.ToString();
            if (zombieCount <= 0)
            {
                LevelComplete();
            }
        }
        
    }

    public void PlayerHealthBar(float playerHealth)
    {
        playerHealth /= 100;
        Player_Health.fillAmount -= playerHealth;
    }

    void LevelComplete()
    {
        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, 1f);
        LevelData.instance.gameGoals[levelNo] = GameGoal.Game_Over;
        SetLevelIndicator();
        levelEnded = true;

    }

    

    void SetLevelIndicator()
    {
        if (levelNo+1 == storePP)
        {
            
            PlayerPrefsController.instance.SetSceneIndex(levelNo+2);
            
        }

        if(levelNo+1 == 5)
        {
            PlayerPrefsController.instance.UnlockCharacter(1);
        }
    }

    #region ButtonMethods

    public void MainMenu()
    {
        if(pauseMenu.activeInHierarchy || successMessage.activeInHierarchy)
        {
            Time.timeScale = 1.0f;
        }
        AudioSource.PlayClipAtPoint(buttons, Camera.main.transform.position, 1f);
        Destroy(mainMenuController.gameObject);
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);

    }

    public void LoadNextLevel()
    {
        AudioSource.PlayClipAtPoint(buttons, Camera.main.transform.position, 1f);
        LevelData.instance.currentLevel = levelNo + 2;
        SceneManager.LoadScene(TagManager.LEVEL_1_NAME);
    }

    #endregion

   

    
}
