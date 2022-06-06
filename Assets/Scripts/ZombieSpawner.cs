using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject zombie;

    [SerializeField]
    GameObject Spawner_FX;

    [SerializeField]
    Transform spawnPositon;

    
    void Start()
    {
        Spawner_FX.SetActive(true);
        SpawnZombie();

    }

    private void SpawnZombie()
    {
        Vector2 offest = new Vector2(spawnPositon.position.x, spawnPositon.position.y + 0.65f);
        var player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
        var thisZombie = Instantiate(zombie,offest, Quaternion.identity);
        thisZombie.transform.localScale = zombie.transform.position.x > player.transform.position.x ? new Vector2(1f, 1f) : new Vector2(-1f, 1f);
        StartCoroutine(DestroySpawner());
    }

    IEnumerator DestroySpawner()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
