using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    public Color hoverColor;
    public Vector3 placementOffset;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        // highlight nodes that can have turrets placed on them
        if (turret == null)
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
        if (turret != null)
        {
            return;
        }

        // build a turret
        var turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + placementOffset, transform.rotation);

        // remove tile highlight immediately
        rend.material.color = startColor;
    }
}
