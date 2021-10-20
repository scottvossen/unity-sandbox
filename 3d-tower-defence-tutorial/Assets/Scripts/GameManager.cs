using Assets.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool GameInProgress;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

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

    public void WinLevel()
    {
        GameInProgress = false;
        completeLevelUI.SetActive(true);
    }
}
