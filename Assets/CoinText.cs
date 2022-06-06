using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "x " + PlayerPrefsController.instance.GetCoin().ToString();
    }

    
    
}
