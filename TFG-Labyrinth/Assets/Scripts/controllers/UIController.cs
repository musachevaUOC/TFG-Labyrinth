using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController inst;
    
    public GameObject controllsUI;
    public GameObject UpgradeUI;
    public GameObject mainMenuUI;
    public GameObject loadScreen;

    public TextMeshProUGUI keyCounter;
    public TextMeshProUGUI CoinCounter;
    
    public UpgradeUIController CoinCounterUpgrade;

    public HeartCounter heartCounter;

    private void Awake()
    {
        if (UIController.inst == null)
        {
            UIController.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void showControllerUI()
    {
        loadScreen.SetActive(false);
        UpgradeUI.SetActive(false);
        mainMenuUI.SetActive(false);
        controllsUI.SetActive(true);
        refreshControllerUI();

    }

    public void showLoadScreen()
    {
        UpgradeUI.SetActive(false);
        mainMenuUI.SetActive(false);
        controllsUI.SetActive(false);
        loadScreen.SetActive(true);
    }

    public void showMainMenu()
    {
        loadScreen.SetActive(false);
        UpgradeUI.SetActive(false);
        controllsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        
    }

    public void showUpgradeUI()
    {
        controllsUI.SetActive(false);
        UpgradeUI.SetActive(true);
        refreshUpgradeCoinCounter();
    }

    public void refreshUpgradeCoinCounter()
    {
        CoinCounterUpgrade.refresh();
    }

    public void refreshControllerUI()
    {
        keyCounter.SetText(" X " + Player.inst.getPickedUpKeys());
        CoinCounter.SetText(" X " + Player.inst.getPickedUpKCoins());
    }

    public void drawHearts(int health)
    {
        heartCounter.drawHearts(health);
    }

}
