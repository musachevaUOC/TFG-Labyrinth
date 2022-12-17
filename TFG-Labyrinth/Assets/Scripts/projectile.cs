using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
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
        Destroy(gameObject);
        enemyController ec = other.gameObject.GetComponent<enemyController>();

        if(ec != null)
        {
            ec.hit(damage);
        }
    }
}
