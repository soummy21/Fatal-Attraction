using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] GameObject characterButton;
    void Awake()
    {

        if (PlayerPrefsController.instance.GetCharacterIndex() == 1)
        {
            characterButton.GetComponent<Button>().enabled = true;
            characterButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
        }

        

    }


}