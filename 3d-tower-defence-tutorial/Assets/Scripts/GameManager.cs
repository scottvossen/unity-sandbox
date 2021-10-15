using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool GameInProgress = true;

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
        Debug.Log("Game Over!");
    }
}
