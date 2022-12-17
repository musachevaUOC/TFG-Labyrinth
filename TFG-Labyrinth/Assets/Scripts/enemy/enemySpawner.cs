using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject enemy;

    public int numberOfEnemies = 4;
    
    
    private float enemySpawnRadius;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnRadius = GetComponent<BoxCollider>().size.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for(int i=0; i < numberOfEnemies; i++)
            {
                Instantiate(enemy, transform.position + new Vector3(Random.Range(-1f,1f)*enemySpawnRadius,0, Random.Range(-1f, 1f) * enemySpawnRadius), transform.rotation).SetActive(true);
            }
        }

        Destroy(gameObject);
    }

}
