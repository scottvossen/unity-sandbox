using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    internal TowerBlueprint selectedTower = null;

    public GameObject buildEffect;

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

        // build the tower
        node.tower = Instantiate(selectedTower.prefab, node.buildPosition, Quaternion.identity);

        // add a build effect and clean up after ourselves
        var effect = Instantiate(buildEffect, node.buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        // pay for the tower
        PlayerStats.Money -= selectedTower.cost;
    }
}
