using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    MainMenuController mainMenuController;

    [SerializeField] AudioClip coinSound;
    private void Start()
    {
        mainMenuController = FindObjectOfType<MainMenuController>();
        StartCoroutine(DestroyCoin());
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == TagManager.PLAYER_HEALTH_TAG || player.tag == TagManager.PLAYER_TAG)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position, 1f);
            mainMenuController.AddCoin();
            Destroy(gameObject);
            
        }
    }

    IEnumerator DestroyCoin()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }



  
}
