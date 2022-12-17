using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    
    public GameObject projectile;

    public int health = 5;

    public float speed = 4f;
    public float distFromPlayerObj = 5f;
    public float shootRate = 2f;
    
    
    private GameObject player;
    
    private float distFromPlayer;
    private float reloadTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distFromPlayer = Vector3.Magnitude(transform.position - player.transform.position);
        reloadTimeLeft = shootRate;
        
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
        if (distFromPlayer > distFromPlayerObj)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);
        shoot();
    }



    public void hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
