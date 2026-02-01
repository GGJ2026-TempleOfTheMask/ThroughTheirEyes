using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image blackScreen;
    [SerializeField] bool inProcess;
    float fadeDuration = 2f;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //For Reference, 0 is title, 1 is temple right now
    public void transitionScene(int sceneIndex)
    {
        if (inProcess) return;
        
        inProcess = true;
        StartCoroutine(FadeAndLoad(sceneIndex));  
    }
    
    public void transitionSceneFast(int sceneIndex)
    {
        inProcess = true;
        SceneManager.LoadScene(sceneIndex);
        inProcess = false;
    }
    IEnumerator FadeAndLoad(int sceneIndex)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        SceneManager.LoadScene(sceneIndex);
        yield return StartCoroutine(Fade(1f, 0f));
        inProcess = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;
        Color c = blackScreen.color;

        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            blackScreen.color = new Color(c.r, c.g, c.b, alpha);

            t += Time.deltaTime;
            yield return null;
        }

        // Snap to final alpha
        blackScreen.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
