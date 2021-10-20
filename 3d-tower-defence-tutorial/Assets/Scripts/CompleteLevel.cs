using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    public string nextLevel = "Level02";
    public int levelUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.LevelReached, levelUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
