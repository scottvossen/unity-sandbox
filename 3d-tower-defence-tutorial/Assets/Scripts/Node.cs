using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private BuildManager buildManager;
    private Renderer rend;
    private Color startColor;

    [Header("Optional")]
    public GameObject turret;
    public Color hoverColor;
    public Vector3 placementOffset;

    public Vector3 buildPosition => transform.position + placementOffset;

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

        // highlight the tile if we have a turret to build and there is not currently a turret on this tile
        if (buildManager.CanBuild && turret == null)
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

        // if we have a turret to build, built it
        if (buildManager.CanBuild)
        {
            buildManager.BuildTower(this);

            // remove tile highlight once it has a turret built on it
            rend.material.color = startColor;
        }
    }
}
