using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        // fade in
        while (t > 0)
        {
            t -= Time.deltaTime;

            // get the alpha value based on our fade curve
            float a = fadeCurve.Evaluate(t);

            // change alpha of black background over time
            image.color = new Color(0f, 0f, 0f, a);

            // skip to the next frame (IE continue)
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        // fade out
        while (t < 1f)
        {
            t += Time.deltaTime;

            // get the alpha value based on our fade curve
            float a = fadeCurve.Evaluate(t);

            // change alpha of black background over time
            image.color = new Color(0f, 0f, 0f, a);

            // skip to the next frame (IE continue)
            yield return 0;
        }

        // transition scene
        SceneManager.LoadScene(scene);
    }
}
