using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public TMPro.TextMeshProUGUI keyUI;
    public TMPro.TextMeshProUGUI coinUI;

    public int health = 5;
    

    public float lookSensitivity = 2f;
    public float speed = 8f;
    
    private int pickedUpKeys = 0;
    private int pickedUpCoins = 0;



    public void pickUpKey()
    {
        pickedUpKeys += 1;
        keyUI.SetText(" X " + pickedUpKeys);
        
    }

    public void pickUpCoin()
    {
        pickedUpCoins += 1;
        coinUI.SetText(" X " + pickedUpCoins);

    }

    public void hit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        
        SceneManager.LoadScene(0);
    }

    public void win()
    {

        SceneManager.LoadScene(0);
    }

    public int getPickedUpKeys()
    {
        return pickedUpKeys;
    }

    public int getPickedUpKCoins()
    {
        return pickedUpCoins;
    }


    
}
