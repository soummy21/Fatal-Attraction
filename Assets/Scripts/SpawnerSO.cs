using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spawner")]

public class SpawnerSO : ScriptableObject
{
    public ZombieSpawner[] zombieSpawners;
   
    public float minSpawnRate;
    public float maxSpawnRate;

    public bool spawnBossOnce = false;
    public float timeToSpawnAt;
    public ZombieSpawner bossSpawner;


}
