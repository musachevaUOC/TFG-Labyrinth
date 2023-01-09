using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuScripts : MonoBehaviour
{

    public Button b;

    void Start()
    {
        b.onClick.AddListener(SceneController.inst.loadFirstLevel);
    }
}
