using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TowerBlueprint selectedTower;

    public static BuildManager instance { get; private set; }

    public bool CanBuild => selectedTower != null;

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
        node.turret = Instantiate(selectedTower.prefab, node.buildPosition, Quaternion.identity);
    }
}
