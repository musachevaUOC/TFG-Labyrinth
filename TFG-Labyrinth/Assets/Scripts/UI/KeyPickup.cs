using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{

    public Player player;

    private void Start()
    {
        if(player == null)
        {
            player = Player.inst;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            player.pickUpKey();
            AudioController.inst.playKeyPickup();
            Destroy(gameObject);
        }
    }
}
