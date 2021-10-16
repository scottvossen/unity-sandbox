using UnityEngine;

public class Shop : MonoBehaviour
{
    // TODO: Add better indicators for which turret is selected

    private BuildManager buildManager;

    public TowerBlueprint standardTurret;
    public TowerBlueprint missileLauncher;
    public TowerBlueprint laserBeamer;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTower(standardTurret);
    }
    public void SelectMissileLauncher()
    {
        buildManager.SelectTower(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTower(laserBeamer);
    }
}
