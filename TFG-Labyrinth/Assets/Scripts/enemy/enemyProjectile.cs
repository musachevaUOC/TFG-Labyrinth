using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{

    public float speed = 5f;
    public float aliveTime = 5f;
    public int damage = 2;

    private float remainTime;

    void Start()
    {
        remainTime = aliveTime;
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(remainTime <= 0f)
        {
            Destroy(gameObject);
        }
        remainTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.hit(damage);
        }
        if (other.tag != "Enemy") { 
            Destroy(gameObject);
        }

        
    }
}
