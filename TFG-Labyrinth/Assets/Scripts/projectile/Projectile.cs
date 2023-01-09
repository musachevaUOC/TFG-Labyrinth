using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    ProjectileBehaviuour projectileBehaviuour;

    private float remainTime;
    private float colliderDelay = .1f;

    public bool isfragment = false;
    public bool isShot = false;

    private void Awake()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    void Start()
    {
        projectileBehaviuour = Player.inst.getProjectileController();
        remainTime = projectileBehaviuour.aliveTime;
        transform.localScale = Vector3.one * projectileBehaviuour.size;


        projectileBehaviuour.performShootAction(this);
        AudioController.inst.playshot(transform.position);

        StartCoroutine(colliderDelayedStart());
    }


    void Update()
    {
        projectileBehaviuour.performActions(this);

        if(remainTime <= 0f)
        {
            Destroy(gameObject);
        }
        remainTime -= Time.deltaTime;
    }

    private IEnumerator colliderDelayedStart()
    {
        yield return new WaitForSeconds(colliderDelay);
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "projectile") {

            if (other.tag == "Enemy")
            {
                enemyController ec = other.gameObject.GetComponent<enemyController>();
                ec.hit(projectileBehaviuour.damage);
                AudioController.inst.playhitEnemy();
            }
            else
            {
                AudioController.inst.playHitEnv(transform.position);
            }
            projectileBehaviuour.performHitActon(this);
        }
    }

}
