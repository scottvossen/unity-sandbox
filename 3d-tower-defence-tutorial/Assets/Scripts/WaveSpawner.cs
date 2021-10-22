using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemyCount = 0;

    private float countdown = 2f;
    private int waveIndex = 0;

    public Wave[] enemyWaves;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public float timeBetweenWaves = 10f;

    public GameManager gameManager;

    public int WavesLeft => enemyWaves.Length - waveIndex + 1;

    private void Start()
    {
        enemyCount = 0;
    }

    private void Update()
    {
        // wait until the previous wave is eliminated
        if (enemyCount > 0)
        {
            return;
        }

        // check if there are no more waves
        if (waveIndex == enemyWaves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }

        // if the previous wave is eliminate, begin the wave counter
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        // reduce the countdown timer by the number of seconds that have passed since the last update
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.RoundsSurvived++;
        var wave = enemyWaves[waveIndex];
        
        enemyCount = wave.enemyCount;

        for (var i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }

        waveIndex++;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
