using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour // singleton scenemanager
{
    
    public static SceneController inst;

    public GameObject UI_gameObject;
    public GameObject mainCharacter;

    public UIController uicontroller;

    AsyncOperation currentScene;
    int currentSceneIndex;


    public void Awake() //init singleton class
    {
        if (SceneController.inst == null)
        {
            SceneController.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void nextScene()
    {
        if(currentSceneIndex < 3)
        {
            //bug with character controller workarround
            UIController.inst.showLoadScreen();
            Player.inst.getCharacterController().enabled = false;
            Player.inst.getPlayerMovementController().enabled = false;

            currentSceneIndex += 1;
            currentScene = SceneManager.LoadSceneAsync(currentSceneIndex);
        }
        else
        {
            returnToMainMenu();
        }
    }

    public void loadFirstLevel()
    {
        currentSceneIndex = 1;
        mainCharacter.SetActive(true);
        UIController.inst.showLoadScreen();
        Player.inst.Start();
        currentScene = SceneManager.LoadSceneAsync(currentSceneIndex);
        SceneManager.sceneLoaded += onMainGameSceneHandler;

    }

    public void returnToMainMenu()
    {
        currentSceneIndex = 0;
        SceneManager.LoadScene(currentSceneIndex);
        SceneManager.sceneLoaded -= onMainGameSceneHandler;
        mainCharacter.SetActive(false);
        UIController.inst.showMainMenu();
    }

    public UIController getUIController()
    {
        return UIController.inst;
    }

    public float getLoadProgress()
    {
        return currentScene.progress;
    }

    // private methods

    private void onMainGameSceneHandler(Scene scene, LoadSceneMode lsm)
    {
        UIController.inst.showControllerUI();
        if (currentSceneIndex == 1)
        {
            LabrinthBuilder lb = GameObject.FindGameObjectWithTag("GameController").GetComponent<LabrinthBuilder>();
            lb.enabled = true;
        }
        else {
            mainCharacter = Player.inst.gameObject;
            UI_gameObject = Player.inst.getPlayerUI().gameObject;

            LabrinthBuilder lb = GameObject.FindGameObjectWithTag("GameController").GetComponent<LabrinthBuilder>();
            lb.enabled = true;
            Player.inst.resetPickedUpKeys();
            Player.inst.ReRollUpgrades();
        }
    }
}
