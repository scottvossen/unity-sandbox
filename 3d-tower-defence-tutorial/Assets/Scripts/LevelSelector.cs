using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;

    public Button[] levelButtons;

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

    private void Start()
    {
        var levelReached = PlayerPrefs.GetInt(PlayerPrefKeys.LevelReached, 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
