using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIController : MonoBehaviour
{

    private Player player;
    private List<Upgrade> upgrades;

    public TMPro.TextMeshProUGUI coinCounter;

    public TMPro.TextMeshProUGUI upgradeText1, upgradeText2, upgradeText3;
    public TMPro.TextMeshProUGUI priceText1, priceText2, priceText3;

    public Button upgrade1, upgrade2, upgrade3;
    public Button backButton;

    void Start()
    {
        
        refresh();

    }

    public void refresh()
    {
        player = Player.inst;
        upgrades = player.GetUpgrades().getAvailablUpgrades();

        backButton.GetComponent<Button>().onClick.AddListener(Player.inst.showControllerUI);

        upgrade1.onClick.AddListener(upgrades[0].buyUpgrade);
        upgrade2.onClick.AddListener(upgrades[1].buyUpgrade);
        upgrade3.onClick.AddListener(upgrades[2].buyUpgrade);

        coinCounter.SetText(" X " + player.getPickedUpKCoins());

        upgradeText1.SetText(upgrades[0].name);
        upgradeText2.SetText(upgrades[1].name);
        upgradeText3.SetText(upgrades[2].name);

        priceText1.SetText(upgrades[0].getCost().ToString());
        priceText2.SetText(upgrades[1].getCost().ToString());
        priceText3.SetText(upgrades[2].getCost().ToString());
    }

    
}
