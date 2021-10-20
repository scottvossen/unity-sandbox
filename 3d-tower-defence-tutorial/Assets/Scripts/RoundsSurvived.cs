using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundsSurvivedText;

    private void Start()
    {
        StartCoroutine(AnimateText());
    }

    // called whenever the gameover UI is enabled
    private void OnEnable()
    {
        roundsSurvivedText.text = PlayerStats.RoundsSurvived.ToString();
    }

    private IEnumerator AnimateText()
    {
        var round = 0;

        // delay a bit before animating
        roundsSurvivedText.text = "0";
        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStats.RoundsSurvived)
        {
            round++;
            roundsSurvivedText.text = round.ToString();

            // delay so the changes are visible
            yield return new WaitForSeconds(0.05f);
        }
    }
}
