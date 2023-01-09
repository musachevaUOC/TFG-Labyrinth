

public class Upgrade
{

    public delegate void UpgradeAction();
    public string name;

    private int cost;
    private UpgradeAction action;
    private bool bought;

    private Player player;

    public Upgrade(int price, UpgradeAction callback, string name)
    {
        this.cost = price;
        this.action = callback;
        this.name = name;

        this.bought = false;
        player = Player.inst;
    }

    public int getCost()
    {
        return this.cost;
    }

    public void buyUpgrade()
    {
        if (!bought && player.getPickedUpKCoins() >= cost)
        {
            name = "¡Adquirido!";
            bought = true;
            this.action();
            player.spendCoins(cost);
            player.GetUpgrades().buyUpgrade(this);
            cost = 0;
        }
        
    }

    public void setBought(bool b)
    {
        this.bought = b;
    }

}
