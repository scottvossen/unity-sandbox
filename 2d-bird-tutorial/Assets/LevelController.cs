using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _currentLevel = 1;

    private Enemy[] _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        Debug.Log("You killed all enemies!");

        // finish the level

        _currentLevel = _currentLevel != 2 
            ? _currentLevel + 1
            : 1;

        var nextLevelName = "Level" + _currentLevel;
        SceneManager.LoadScene(nextLevelName);
    }
}
