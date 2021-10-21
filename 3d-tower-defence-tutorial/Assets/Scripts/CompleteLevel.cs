using Assets.Scripts;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    public string nextLevel;
    public int levelUnlock = 0;

    public void Continue()
    {
        if (levelUnlock > 0)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LevelReached, levelUnlock);
        }

        if (!string.IsNullOrWhiteSpace(nextLevel))
        {
            sceneFader.FadeTo(nextLevel);
        }
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
