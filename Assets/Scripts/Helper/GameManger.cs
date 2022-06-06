using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    [SerializeField] GameObject marry;
    
    [HideInInspector]
    public int playerIndex;
    private void Awake()
    {
        MakeSingleton();
        playerIndex = 0;
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
                
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        
        if(scene.name != TagManager.MAIN_MENU_NAME)
        {
            if(playerIndex != 0)
            {

                var tommy = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
                Instantiate(marry, tommy.transform.position, Quaternion.identity);
                tommy.SetActive(false);
            }
        }
    }

    

}
