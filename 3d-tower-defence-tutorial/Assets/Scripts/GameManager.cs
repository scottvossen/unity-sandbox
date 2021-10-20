using Assets.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool GameInProgress;

    public GameObject gameOverUI;
    public SceneFader sceneFader;
    public string nextLevel = "Level02";
    public int levelUnlock = 2;

    public void WinLevel()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.LevelReached, levelUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    private void Start()
    {
        GameInProgress = true;
    }

    void Update()
    {
        if (!GameInProgress)
        {
            return;
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameInProgress = false;
        gameOverUI.SetActive(true);
    }
}
