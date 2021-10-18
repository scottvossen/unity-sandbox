using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private Node selectedNode = null;
    private TowerBlueprint selectedTower = null;

    public GameObject buildEffect;
    public NodeUI nodeUI;

    public static BuildManager instance { get; private set; }

    public bool HasSelectedNode => selectedNode != null;

    public bool HasSelectedTower => selectedTower != null;

    public bool CanBuildSelectedTower => HasSelectedTower && PlayerStats.Money >= selectedTower.cost;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene.");
            return;
        }

        instance = this;
    }

    public void SelectTower(TowerBlueprint tower)
    {
        // toggle off tower management mode
        DeselectNode();

        selectedTower = tower;
    }

    public void SelectNode(Node node)
    {
        // toggle off tower selection mode
        selectedTower = null;

        // if we're selecting the same node twice, we should instead deselect it
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        // select the node
        selectedNode = node;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void BuildTower(Node node)
    {
        if (!CanBuildSelectedTower)
        {
            return;
        }

        // build the tower
        node.tower = Instantiate(selectedTower.prefab, node.buildPosition, Quaternion.identity);

        // add a build effect and clean up after ourselves
        var effect = Instantiate(buildEffect, node.buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        // pay for the tower
        PlayerStats.Money -= selectedTower.cost;
    }
}
