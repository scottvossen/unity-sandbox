using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    private float countdown = 2f;
    private int waveIndex = 0;

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public float timeBetweenWaves = 5.5f;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // reduce the countdown timer by the number of seconds that have passed since the last update
        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Floor(countdown + 1).ToString();
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;

        for (var i = 0; i < waveIndex; i++)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemies()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
