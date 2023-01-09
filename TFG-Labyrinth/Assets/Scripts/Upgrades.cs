using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    Player player;
    ProjectileBehaviuour projectile;

    private List<Upgrade> totalUpgrades;
    private List<Upgrade> PurchasedUpgrades;
    private List<Upgrade> AvailableUpgrades;

    private readonly int UPGRADE_COUNT = 3;

    public Upgrades()
    {
        player = Player.inst;
        projectile = player.getProjectileController();

        totalUpgrades = new List<Upgrade>();
        PurchasedUpgrades = new List<Upgrade>();
        AvailableUpgrades = new List<Upgrade>();

        InitAvailableUpgradesList();
        setRandomAvailableUpgrades();
    }

    public List<Upgrade> getAvailablUpgrades()
    {
        return this.AvailableUpgrades;
    }
    
    public void setRandomAvailableUpgrades()
    {
        foreach (Upgrade u in AvailableUpgrades)
        {
            PurchasedUpgrades.Add(u);
            totalUpgrades.Remove(u);
        }
        
        AvailableUpgrades.Clear();
        
        for (int i = 0; i < UPGRADE_COUNT; i++)
        {
            int rand = Random.Range(0, totalUpgrades.Count);
            AvailableUpgrades.Add(totalUpgrades[rand]);
            totalUpgrades.RemoveAt(rand);
        }
    }

    public void buyUpgrade(Upgrade up)
    {
        this.PurchasedUpgrades.Add(up);
    }

    //util

    private void InitAvailableUpgradesList()
    {
        totalUpgrades.Add(new Upgrade(2, increaseDamage, "Daño"));
        totalUpgrades.Add(new Upgrade(2, increaseHealth, "Salud"));
        totalUpgrades.Add(new Upgrade(3, increaseSpeed, "Velocidad"));
        totalUpgrades.Add(new Upgrade(4, IncreaseLuck, "Fortuna"));
        totalUpgrades.Add(new Upgrade(1, bulletPierce, "Perforador"));
        totalUpgrades.Add(new Upgrade(5, bulletExplode, "Explosivo"));
        totalUpgrades.Add(new Upgrade(3, multiBullet, "Cañón triple"));
        totalUpgrades.Add(new Upgrade(2, increaseBulletSpeed, "Balas rápidas"));
        totalUpgrades.Add(new Upgrade(5, increaseBulletSize, "Balas gigantes"));
    }

    




    //Upgrade actions

    private void increaseDamage()
    {
        projectile.damage += 2;
    }

    private void increaseHealth()
    {
        player.health += 3;
    }

    private void increaseSpeed()
    {
        player.speed *= 2f;
    }

    private void IncreaseLuck()
    {
        player.lootChance += player.lootChance / 2f;
    }

    private void bulletPierce()
    {
        projectile.addProjectileHitAction(projectile.hitActionPierce);
    }

    private void bulletExplode()
    {
        projectile.addProjectileHitAction(projectile.hitActionExplode);
    }

    private void multiBullet()
    {
        projectile.addProjectileShootAction(projectile.shootActionMulti);
    }

    private void increaseBulletSpeed()
    {
        projectile.speed *= 2;
    }

    private void increaseBulletSize()
    {
        projectile.size = 1f;
    }

}
