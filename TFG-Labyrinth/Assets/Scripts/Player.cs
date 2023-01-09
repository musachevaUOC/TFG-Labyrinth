using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //singleton player
{
    public static Player inst;

    public int health = 5;

    public float lookSensitivity = 2f;
    public float speed = 8f;

    public float lootChance = 0.5f;

    private SceneController sc;

    private Movement movement;
    private CharacterController playerCharacterController;
    private ProjectileBehaviuour projectile;

    private int pickedUpKeys;
    private int pickedUpCoins;

    private Upgrades upgrades;

    private UIController controllerUI;

    
    public void Awake() //init singleton class
    {
        if(Player.inst == null)
        {
            Player.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        health = 5;
        speed = 8f;
        lootChance = 0.5f;
        pickedUpKeys = 0;
        pickedUpCoins = 0;

        sc = SceneController.inst;

        this.controllerUI = sc.getUIController();

        movement = GetComponent<Movement>();
        playerCharacterController = GetComponent<CharacterController>();

        projectile = new ProjectileBehaviuour(8f, 5f, 1, 0.25f);
        upgrades = new Upgrades();

        projectile.addProjectileAction(projectile.simpleMove);
        projectile.addProjectileHitAction(projectile.hitActionNothing);
        projectile.addProjectileShootAction(projectile.shootActionNone);

        controllerUI.refreshControllerUI();
        drawHearts();
    }

    private void OnDestroy()
    {
        Destroy(controllerUI.gameObject);
    }
    public void pickUpKey()
    {
        pickedUpKeys += 1;
        controllerUI.refreshControllerUI();

    }

    public void pickUpCoin()
    {
        pickedUpCoins += 1;
        controllerUI.refreshControllerUI();

    }

    public void resetPickedUpKeys()
    {
        pickedUpKeys = 0;
        controllerUI.refreshControllerUI();
    }

    public void spendCoins(int n)
    {
        pickedUpCoins -= n;
        controllerUI.refreshUpgradeCoinCounter();
    }

    public void hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
        else {
            drawHearts();
        }
    }

    public void drawHearts()
    {
        controllerUI.drawHearts(health);
    }

    public void die()
    {

        sc.returnToMainMenu();
        
    }

    public void win()
    {
        sc.nextScene();
    }

    public int getPickedUpKeys()
    {
        return pickedUpKeys;
    }

    public int getPickedUpKCoins()
    {
        return pickedUpCoins;
    }

    public float getPlayerLootChance()
    {
        return lootChance;
    }

    
    public Transform getPlayerUI()
    {
        return controllerUI.transform;
    }


    public void showControllerUI() {

        controllerUI.showControllerUI();
        controllerUI.refreshControllerUI();
    }

    public void showUpgradeUI()
    {
        controllerUI.showUpgradeUI();
    }

    public Movement getPlayerMovementController()
    {
        return movement;
    }

    public CharacterController getCharacterController()
    {
        return playerCharacterController;
    }

    public ProjectileBehaviuour getProjectileController()
    {
        return projectile;
    }

    public Upgrades GetUpgrades()
    {
        return upgrades;
    }


    public void ReRollUpgrades()
    {
        upgrades.setRandomAvailableUpgrades();
        controllerUI.refreshUpgradeCoinCounter();
    }

    
}
