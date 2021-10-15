using UnityEngine;

public class Shop : MonoBehaviour
{
    // TODO: Add better indicators for which turret is selected

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseMissileLauncher()
    {
        buildManager.SetTurretToBuild(buildManager.missibleLauncherPrefab);
    }
}
