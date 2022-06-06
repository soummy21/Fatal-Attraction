using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public static PlayerPrefsController instance;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt(TagManager.SCENE_INDEX_KEY, 1));
        MakeSingleton();
    }
    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetSceneIndex(int index)
    {
        PlayerPrefs.SetInt(TagManager.SCENE_INDEX_KEY, index);
    }

    public int GetSceneIndex()
    {
        return PlayerPrefs.GetInt(TagManager.SCENE_INDEX_KEY, 1);
    }


    

    public void SetCoin(int coin)
    {
        PlayerPrefs.SetInt(TagManager.COIN_KEY, coin);
    }

    public int GetCoin()
    {
        return PlayerPrefs.GetInt(TagManager.COIN_KEY, 0);
    }

    public void UnlockCharacter(int charIndex)
    {
        PlayerPrefs.SetInt(TagManager.UNLOCK_RITUZA_KEY,charIndex);
    }

    public int GetCharacterIndex()
    {
       return PlayerPrefs.GetInt(TagManager.UNLOCK_RITUZA_KEY, 0);
    }
}
