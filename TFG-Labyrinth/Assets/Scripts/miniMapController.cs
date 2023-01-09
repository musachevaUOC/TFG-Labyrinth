using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapController : MonoBehaviour
{
    Player player;


    void Start()
    {
        player = Player.inst;
    }

    // Update is called once per frame
    void Update()
    {
        if(player!= null)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
        
    }
}
