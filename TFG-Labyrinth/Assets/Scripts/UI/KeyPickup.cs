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
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            player.pickUpKey();
            Destroy(gameObject);
        }
    }
}
