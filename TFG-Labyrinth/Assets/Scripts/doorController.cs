using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public GameObject keyHole;

    public float sepacing = 0.5f;

    private int keyAmmount;
    private int keys = 0;

    private Stack<GameObject> keyHoles;
    
    // Start is called before the first frame update
    void Start()
    {
        keyHoles = new Stack<GameObject>();
        keyAmmount = GameObject.FindGameObjectWithTag("GameController").GetComponent<LabrinthBuilder>().keysNumber;

        for(int i=0; i< keyAmmount; i++)
        {
            GameObject g = Instantiate(keyHole, keyHole.transform.position - Vector3.forward* sepacing * i, keyHole.transform.rotation, transform);
            g.SetActive(true);
            keyHoles.Push(g);
        }
    }

    void placeKeys(int n)
    {
        for(int i = 0; i < n; i++)
        {
            GameObject g = keyHoles.Pop();
            g.transform.GetChild(0).gameObject.SetActive(true);
            keys++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            int playesrKeys = player.getPickedUpKeys();

            if (playesrKeys < keyAmmount)
            {
                placeKeys(playesrKeys - keys);
            }
            else
            {
                player.win();
            }
        }
    }


}
