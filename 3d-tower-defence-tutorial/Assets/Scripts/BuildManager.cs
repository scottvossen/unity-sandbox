using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    internal TowerBlueprint selectedTower = null;

    public static BuildManager instance { get; private set; }

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
        selectedTower = tower;
    }

    public void BuildTower(Node node)
    {
        if (!CanBuildSelectedTower)
        {
            return;
        }

        node.tower = Instantiate(selectedTower.prefab, node.buildPosition, Quaternion.identity);

        PlayerStats.Money -= selectedTower.cost;
    }
}
