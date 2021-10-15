using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private BuildManager buildManager;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    public Color hoverColor;
    public Vector3 placementOffset;

    private void Start()
    {
        buildManager = BuildManager.instance;

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        // if we're hovering over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        var turretToBuild = buildManager.GetTurretToBuild();

        // highlight the tile if we have a turret to build and there is not currently a turret on it
        if (turret == null && turretToBuild != null)
        {
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        // if we're hovering over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // do nothing if this tile already has a turret
        if (turret != null)
        {
            return;
        }

        var turretToBuild = buildManager.GetTurretToBuild();

        // if we have a turret to build, built it
        if (turretToBuild != null)
        {
            turret = Instantiate(turretToBuild, transform.position + placementOffset, transform.rotation);

            // remove tile highlight once it has a turret built on it
            rend.material.color = startColor;
        }
    }
}
