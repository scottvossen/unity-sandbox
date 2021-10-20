using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsSurvivedText;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    // called whenever the gameover UI is enabled
    private void OnEnable()
    {
        roundsSurvivedText.text = PlayerStats.RoundsSurvived.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
