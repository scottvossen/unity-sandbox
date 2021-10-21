using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private BuildManager buildManager;
    private Renderer rend;
    private Color startColor;
    private bool towerPlacementIsHovering = false;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint blueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Color hoverColor;
    public Color insufficientFundsColor;
    public Vector3 placementOffset;

    public Vector3 buildPosition => transform.position + placementOffset;

    public void UpgradeTower()
    {
        if (isUpgraded)
        {
            return;
        }

        if (PlayerStats.Money < blueprint.upgradeCost)
        {
            return;
        }

        // replace the tower with it's upgraded version
        var oldTower = tower;
        tower = Instantiate(blueprint.upgradedPrefab, buildPosition, Quaternion.identity);
        Destroy(oldTower);

        // add a build effect and clean up after ourselves
        var effect = Instantiate(buildManager.buildEffect, buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        // pay for the upgrade
        PlayerStats.Money -= blueprint.upgradeCost;

        isUpgraded = true;
    }

    public void SellTower()
    {
        // pay that man his money
        PlayerStats.Money += isUpgraded 
            ? blueprint.upgradedSellValue 
            : blueprint.sellValue;

        // remove the tower
        Destroy(tower);

        // do a cool effect
        var effect = Instantiate(buildManager.sellEffect, buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        // reset the tile state
        tower = null;
        blueprint = null;
        isUpgraded = false;
    }

    private void Start()
    {
        buildManager = BuildManager.instance;

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void Update()
    {
        if (towerPlacementIsHovering)
        {
            SetHoverColor();
        }
    }

    private void OnMouseEnter()
    {
        // if we're hovering over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // highlight the tile if we have a turret to build and there is not currently a turret on this tile
        if (buildManager.HasSelectedTower && tower == null)
        {
            towerPlacementIsHovering = true;
            SetHoverColor();
        }
    }

    private void OnMouseExit()
    {
        towerPlacementIsHovering = false;
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        // if we're hovering over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // select the node if the tile already has a turret
        if (tower != null)
        {
            SelectNodeForTowerManagement();
            return;
        }

        // if we have a turret to build, built it
        if (buildManager.CanBuildSelectedTower)
        {
            BuildTower(buildManager.GetSelectedTower());
        }
    }

    private void SelectNodeForTowerManagement()
    {
        buildManager.SelectNode(this);
    }

    private void BuildTower(TowerBlueprint t)
    {
        if (!buildManager.CanBuildSelectedTower)
        {
            return;
        }

        // build the tower
        tower = Instantiate(t.prefab, buildPosition, Quaternion.identity);
        blueprint = t;

        // add a build effect and clean up after ourselves
        var effect = Instantiate(buildManager.buildEffect, buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        // pay for the tower
        PlayerStats.Money -= t.cost;

        // remove tile highlight once it has a turret built on it
        rend.material.color = startColor;
    }

    private void SetHoverColor()
    {
        rend.material.color = buildManager.CanBuildSelectedTower
            ? hoverColor
            : insufficientFundsColor;
    }
}
