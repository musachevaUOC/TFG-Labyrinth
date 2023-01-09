using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgStandController : MonoBehaviour
{
    public GameObject upgradeUI;

    private Player player;


    private void Start()
    {
        player = Player.inst;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.showUpgradeUI();
        }
    }

}
