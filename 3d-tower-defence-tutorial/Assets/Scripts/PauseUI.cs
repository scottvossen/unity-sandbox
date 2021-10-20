using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private int keypressThrottleTimeMs = 250;
    private Stopwatch keypressThrottleTimer;

    public GameObject pauseUI;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    private void Start()
    {
        keypressThrottleTimer = Stopwatch.StartNew();
    }

    private void Update()
    {
        if (keypressThrottleTimer.ElapsedMilliseconds >= keypressThrottleTimeMs)
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Toggle();
                keypressThrottleTimer.Restart();
            }
        }
    }

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        Time.timeScale = pauseUI.activeSelf ? 0 : 1;
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
