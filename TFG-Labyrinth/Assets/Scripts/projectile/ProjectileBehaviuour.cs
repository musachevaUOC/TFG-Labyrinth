using UnityEngine;

public class ProjectileBehaviuour
{
    public float speed;
    public float aliveTime;
    public int damage;
    public float size;

    public delegate void projectileActions(Projectile p);

    private projectileActions actionsHandler;
    private projectileActions actionsHandlerHit;
    private projectileActions actionsHandlerShoot;

    public ProjectileBehaviuour(float speed, float aliveTime, int damage, float size)
    {
        this.speed = speed;
        this.aliveTime = aliveTime;
        this.damage = damage;
        this.size = size;
    }



    public void performActions(Projectile p)
    {
        actionsHandler(p);
    }

    public void performHitActon(Projectile p)
    {
        actionsHandlerHit(p);
    }

    public void performShootAction(Projectile p)
    {
        actionsHandlerShoot(p);
    }




    public void addProjectileAction(projectileActions pa)
    {
        actionsHandler += pa;
    }
    public void addProjectileHitAction(projectileActions pa)
    {
        actionsHandlerHit = pa;
    }
    public void addProjectileShootAction(projectileActions pa)
    {
        actionsHandlerShoot = pa;
    }


    // **** behaviour methods definitions ****

    // move methods

    public void simpleMove(Projectile p)
    {
        p.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    // hit actions

    public void hitActionPierce(Projectile p)
    {
        return;
    }

    public void hitActionNothing(Projectile p)
    {
        MonoBehaviour.Destroy(p.gameObject);
    }

    public void hitActionExplode(Projectile p)
    {
        if (!p.isfragment)
        {
            for (int i = 0; i<5;i++)
            {
                float rot = (360f/5f) * i + Random.Range(0,50);
                Projectile newProjectile = MonoBehaviour.Instantiate(p.gameObject, p.transform.position, Quaternion.Euler(0, rot, 0)).GetComponent<Projectile>();
                newProjectile.isfragment = true;
            }
        }
        

        MonoBehaviour.Destroy(p.gameObject);
    }

    // shoot actions

    public void shootActionNone(Projectile p)
    {
        return;
    }

    public void shootActionMulti(Projectile p)
    {
        if (!p.isShot)
        {
            for (int i = 0; i < 3; i++)
            {
                float rot = 10f * i - 10f;
                Quaternion qrot = Quaternion.Euler(0,p.transform.eulerAngles.y + rot,0);
                Projectile newProjectile = MonoBehaviour.Instantiate(p.gameObject, p.transform.position, qrot).GetComponent<Projectile>();
                newProjectile.isShot = true;
            }
            MonoBehaviour.Destroy(p.gameObject);
        }
    }
}
