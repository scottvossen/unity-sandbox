using UnityEngine;

public class NodeUI : MonoBehaviour
{
    private Node targetNode;
    
    public GameObject ui;

    public void SetTarget(Node target)
    {
        // set the target
        targetNode = target;

        // move our game object to hover over the node
        transform.position = target.buildPosition;

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
