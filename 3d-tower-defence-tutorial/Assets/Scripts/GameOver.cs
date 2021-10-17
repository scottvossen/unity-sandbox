using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsSurvivedText;

    // called whenever the gameover UI is enabled
    private void OnEnable()
    {
        roundsSurvivedText.text = PlayerStats.RoundsSurvived.ToString();
    }

    public void Retry() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go to menu...");
    }
}
