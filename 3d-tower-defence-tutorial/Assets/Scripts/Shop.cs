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
            selectedTowerBackground.color = SetRgb(selectedTowerBackground.color, getSelectedTowerColor());
        }
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTower(standardTurret);
        selectTower(standardTurret, standardTurretButtonBackground);
    }
    public void SelectMissileLauncher()
    {
        buildManager.SelectTower(missileLauncher);
        selectTower(missileLauncher, missileLauncherButtonBackground);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTower(laserBeamer);
        selectTower(laserBeamer, laserBeamerButtonBackground);
    }

    public void clearTowerSelection()
    {
        standardTurretButtonBackground.enabled = false;
        missileLauncherButtonBackground.enabled = false;
        laserBeamerButtonBackground.enabled = false;

        selectedTowerBackground = null;
    }

    private void selectTower(TowerBlueprint tower, Image background)
    {
        buildManager.SelectTower(tower);

        clearTowerSelection();

        background.color = SetRgb(background.color, getSelectedTowerColor());
        background.enabled = true;

        selectedTowerBackground = background;
    }

    private Color getSelectedTowerColor()
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
