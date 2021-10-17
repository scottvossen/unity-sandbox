using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool GameInProgress;

    public GameObject gameOverUI;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
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
