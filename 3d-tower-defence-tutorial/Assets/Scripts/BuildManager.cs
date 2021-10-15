using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject turretToBuild;

    public static BuildManager instance { get; private set; }

    public GameObject standardTurretPrefab;
    public GameObject missibleLauncherPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene.");
            return;
        }

        instance = this;
    }

    public GameObject GetTurretToBuild() => turretToBuild;

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
