using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject enemy;
    
    private float enemySpawnRadius;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource aus = GetComponent<AudioSource>();
            Animator anim = GetComponent<Animator>();
            SphereCollider sc = GetComponent<SphereCollider>();

            aus.enabled = true;
            anim.enabled = true;
            sc.enabled = false;

        }

        
    }

    public void spawnEnemy()
    {
        Instantiate(enemy, transform.position + Vector3.up*2.5f, transform.rotation);
        gameObject.SetActive(false);
    }

}
