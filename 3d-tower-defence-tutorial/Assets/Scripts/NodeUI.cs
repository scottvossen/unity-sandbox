using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node targetNode;
    
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellValue;
    public Button sellButton;

    public void SetTarget(Node target)
    {
        // set the target
        targetNode = target;

        // move our game object to hover over the node
        transform.position = target.buildPosition;

        // update the menu items
        upgradeButton.interactable = !target.isUpgraded;
        upgradeCost.text = targetNode.isUpgraded 
            ? "MAX" 
            : $"${target.blueprint.upgradeCost}";


        sellValue.text = targetNode.isUpgraded
            ? $"${targetNode.blueprint.upgradedSellValue}"
            : $"${targetNode.blueprint.sellValue}";

        // make the UI visible
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        targetNode.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        targetNode.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
