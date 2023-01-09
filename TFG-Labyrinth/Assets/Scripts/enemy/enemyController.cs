using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{

    
    public GameObject projectile;
    public GameObject loot;
    public GameObject enemyDead;

    public int health = 5;

    public float shootRate = 2f;
    
    
    private GameObject player;
    private Player playerStats;

    private NavMeshAgent agent;
    
    private float reloadTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reloadTimeLeft = shootRate;
        playerStats = player.GetComponent<Player>();

        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void shoot()
    {
        if (reloadTimeLeft <= 0)
        {
            Instantiate(projectile, transform.position + transform.forward * 1f, transform.rotation).SetActive(true);
            reloadTimeLeft = shootRate;
        }
        reloadTimeLeft -= Time.deltaTime;
    }


    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.transform.position);
            shoot();
        }
        
    }



    public void hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (Random.Range(0f,1f) < playerStats.getPlayerLootChance())
            {
                Instantiate(loot, transform.position, transform.rotation);
            }
            Instantiate(enemyDead, transform.position - Vector3.up * 2.5f, transform.rotation);
            Destroy(gameObject);
        }
    }
}
