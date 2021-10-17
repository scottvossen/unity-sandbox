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
    public float timeBetweenWaves = 20f;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // reduce the countdown timer by the number of seconds that have passed since the last update
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.RoundsSurvived++;

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
