using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayAudio : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<GameplayAudio>().Length>1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == TagManager.MAIN_MENU_NAME)
        {
            Destroy(gameObject);
        }

     
        
    }
}
