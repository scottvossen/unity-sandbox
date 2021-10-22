using UnityEngine;
using UnityEngine.UI;

public class WavesLeft : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public Text wavesLeftText;

    private void Update()
    {
        wavesLeftText.text = waveSpawner.WavesLeft.ToString();
    }
}
