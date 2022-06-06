using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    //public static MainMenuController instance;

    [SerializeField] public GameObject mainMenu;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject story;
    [SerializeField] GameObject characters;
    [SerializeField] GameObject shop;

    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] AudioClip click, back, buy;

    
    int coins;
    GameObject[] panels;

    int count;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    

    #region MenuPanelSelectionMethods
    public void OpenMainMenu()
    {
        
        mainMenu.gameObject.SetActive(true);
        
    }

    public void ControlsMenu()
    {
        
        controls.gameObject.SetActive(true);
        
    }

    public void StoryMenu()
    {
        
        story.gameObject.SetActive(true);
        
    }

    public void CharactersMenu()
    {
        
        characters.gameObject.SetActive(true);
        
    }

    public void ShopMenu()
    {
        
        shop.gameObject.SetActive(true);
        
    }

    #endregion
    public void ChooseMudur()
    {
        
        GameManger.instance.playerIndex = 0;
    }

    public void ChooseRituza()
    {
        
        GameManger.instance.playerIndex = 1;
    }

    private void Start()
    {
        coins = PlayerPrefsController.instance.GetCoin();

        if (PlayerPrefs.GetString(TagManager.UNLOCK_M3_KEY, "NONE") == "YES0")
        {
            buttons[0].GetComponent<Button>().enabled = false;
            buttons[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }

         if (PlayerPrefs.GetString(TagManager.UNLOCK_AK_KEY, "NONE1") == "YES1")
        {
            buttons[1].GetComponent<Button>().enabled = false;
            buttons[1].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_FIRE_KEY, "NONE2") == "YES2")
        {
            buttons[2].GetComponent<Button>().enabled = false;
            buttons[2].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_SNIPER_KEY, "NONE3") == "YES3")
        {
            buttons[3].GetComponent<Button>().enabled = false;
            buttons[3].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
        
        if (PlayerPrefs.GetString(TagManager.UNLOCK_ROCKET_KEY, "NONE4") == "YES4")
        {
            buttons[4].GetComponent<Button>().enabled = false;
            buttons[4].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }

        panels = new GameObject[] { mainMenu, controls, story, characters, shop };
       

    }

    
    public void AddCoin()
    {
        coins++;
        PlayerPrefsController.instance.SetCoin(coins);
        
    }

     void SubtractCoin(int coinsUsed)
    {
        
        coins -= coinsUsed;
        AudioSource.PlayClipAtPoint(buy, Camera.main.transform.position, 0.8f);
        PlayerPrefsController.instance.SetCoin(coins);
        coinText.text = "x " + coins.ToString();
        
    }

    public void BackButton()
    {
        
        for (int i=0;i<panels.Length;i++)
        {
            if(panels[i].activeInHierarchy)
            {
                panels[i].SetActive(false);
                
            }

            if(shop.activeInHierarchy || characters.activeInHierarchy)
            {
                mainMenu.SetActive(true);
            }
        }
    }

    #region GameSceneLoaders
    
    public void LoadLevel(int levelNo)
    {
        
        SceneManager.LoadScene(TagManager.LEVEL_1_NAME);
        LevelData.instance.currentLevel = levelNo;
    }

  
    
    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }

    #region ShopButtons

    public void BuyAK47()
    {
        if (coins >= 80)
        {
            PlayerPrefs.SetString(TagManager.UNLOCK_AK_KEY, "YES1");
            SubtractCoin(80);
            buttons[1].GetComponent<Button>().enabled = false;
            buttons[1].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
    }

    public void BuyM3()
    {
        if (coins >= 40)
        {
            PlayerPrefs.SetString(TagManager.UNLOCK_M3_KEY, "YES0");
            SubtractCoin(40);
            buttons[0].GetComponent<Button>().enabled = false;
            buttons[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
    }

    public void BuyFireGun()
    {
        if (coins >= 100)
        {
            PlayerPrefs.SetString(TagManager.UNLOCK_FIRE_KEY, "YES2");
            SubtractCoin(100);
            buttons[2].GetComponent<Button>().enabled = false;
            buttons[2].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
    }

    public void BuySniper()
    {
        if (coins >= 150)
        {
            PlayerPrefs.SetString(TagManager.UNLOCK_SNIPER_KEY, "YES3");
            SubtractCoin(150);
            buttons[3].GetComponent<Button>().enabled = false;
            buttons[3].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
    }

    public void BuyRocket()
    {
        if (coins >= 200 )
        {
            PlayerPrefs.SetString(TagManager.UNLOCK_ROCKET_KEY, "YES4");
            SubtractCoin(200);
            buttons[4].GetComponent<Button>().enabled = false;
            buttons[4].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }

    }

    #endregion

    
    

    public void ClickSound()
    {
        AudioSource.PlayClipAtPoint(click, Camera.main.transform.position, 0.7f);
    }

  

    public void PlayBackSound()
    {
        
        AudioSource.PlayClipAtPoint(back, Camera.main.transform.position, 0.7f);
    }
}
