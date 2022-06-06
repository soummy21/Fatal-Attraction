using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    //Ref to SO
    [SerializeField] SpawnerSO[] spawnerSO;

    //Camera Reference
    Camera mainCamera;

    [SerializeField]
    ZombieSpawner[] zombieSpawners;
    float minSpawnRate;
    float maxSpawnRate;
    bool spawnBossOnce = false;
    float timeToSpawnAt;
    ZombieSpawner bossSpawner;
    float yMin = -3.7f, yMax = 0.6f;

    private void Start()
    {
        InitializeAllVariables();
        mainCamera = Camera.main;
        InvokeRepeating("StartSpawner", 1f, Random.Range(minSpawnRate, maxSpawnRate+1));

        if(spawnBossOnce)
        {
            Invoke("StartBossSpawner", timeToSpawnAt);
        }
    }
    void InitializeAllVariables()
    {
        var index = LevelData.instance.currentLevel - 1;
        zombieSpawners = spawnerSO[index].zombieSpawners;
        minSpawnRate = spawnerSO[index].minSpawnRate;
        maxSpawnRate = spawnerSO[index].maxSpawnRate;
        spawnBossOnce = spawnerSO[index].spawnBossOnce;
        timeToSpawnAt = spawnerSO[index].timeToSpawnAt;
        bossSpawner = spawnerSO[index].bossSpawner;

    }
    void StartSpawner()
    {
        float yPos = Random.Range(yMin, yMax);
        float xPos = mainCamera.transform.position.x;
        if (LevelData.instance.gameGoals[LevelData.instance.currentLevel-1] == GameGoal.Protect_Fence)
        {
            xPos += 10f;
        }
        else if(LevelData.instance.gameGoals[LevelData.instance.currentLevel-1] == GameGoal.Travel_Distance)
        {

            if (Random.Range(0, 4) > 0)
            {
                xPos += Random.Range(7f, 9f);
            }
            else
            {
                xPos -= Random.Range(7f, 9f);
            }
        }
        else
        {
            if (Random.Range(0, 2) > 0)
            {
                xPos += Random.Range(7f, 9f);
            }
            else
            {
                xPos -= Random.Range(7f, 9f);
            }

        }



        if (LevelData.instance.gameGoals[LevelData.instance.currentLevel-1] == GameGoal.Game_Over)
        {
            CancelInvoke("StartSpawner");
        }else
        {
            Instantiate(zombieSpawners[Random.Range(0, zombieSpawners.Length)], new Vector2(xPos, yPos), Quaternion.identity);
        }
        
    }

    void StartBossSpawner()
    {
        float yPos = Random.Range(yMin, yMax);
        float xPos = mainCamera.transform.position.x;

        if (Random.Range(0, 2) > 0)
        {
            xPos += Random.Range(7f, 9f);
        }
        else
        {
            xPos -= Random.Range(7f, 9f);
        }


        
            Instantiate(bossSpawner, new Vector2(xPos, yPos), Quaternion.identity);
        

    }

}
