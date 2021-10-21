using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    private Image selectedTowerBackground;

    public TowerBlueprint standardTurret;
    public TowerBlueprint missileLauncher;
    public TowerBlueprint laserBeamer;

    public Image standardTurretButtonBackground;
    public Image missileLauncherButtonBackground;
    public Image laserBeamerButtonBackground;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if (selectedTowerBackground != null)
        {
            selectedTowerBackground.color = SetRgb(selectedTowerBackground.color, GetSelectedTowerColor());
        }
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTower(standardTurret);
        SelectTower(standardTurret, standardTurretButtonBackground);
    }
    public void SelectMissileLauncher()
    {
        buildManager.SelectTower(missileLauncher);
        SelectTower(missileLauncher, missileLauncherButtonBackground);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTower(laserBeamer);
        SelectTower(laserBeamer, laserBeamerButtonBackground);
    }

    public void ClearTowerSelection()
    {
        standardTurretButtonBackground.enabled = false;
        missileLauncherButtonBackground.enabled = false;
        laserBeamerButtonBackground.enabled = false;

        selectedTowerBackground = null;
    }

    private void SelectTower(TowerBlueprint tower, Image background)
    {
        buildManager.SelectTower(tower);

        ClearTowerSelection();

        background.color = SetRgb(background.color, GetSelectedTowerColor());
        background.enabled = true;

        selectedTowerBackground = background;
    }

    private Color GetSelectedTowerColor()
    {
        return buildManager.CanBuildSelectedTower ? Color.white : Color.red;
    }

    private Color SetRgb(Color source, Color value)
    {
        var color = value;
        color.a = source.a;
        return color;
    }
}
