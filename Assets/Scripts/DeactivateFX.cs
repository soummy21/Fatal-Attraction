using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableFX", 1f); //Invoke only takes function as a string
    }
    void DisableFX()
    {
        gameObject.SetActive(false); //Makes it reusable in nature
    }
}
