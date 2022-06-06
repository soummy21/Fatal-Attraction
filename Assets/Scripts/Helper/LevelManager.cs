using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelButtons;
    
    void Start()
    {
        for(int i=0;i<PlayerPrefsController.instance.GetSceneIndex();i++)
        {
            levelButtons[i].SetActive(true);
        }
    }

}
