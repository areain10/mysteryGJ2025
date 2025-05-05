using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingText : MonoBehaviour
{
    public List<string> endingWords;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        foreach (string line in endingWords)
        {
            text.text = line;

            yield return StartCoroutine(FadeTextToFullAlpha(1f, text));

            float holdTime = Mathf.Clamp(line.Length * 0.1f, 1f, 5f); 
            yield return new WaitForSeconds(holdTime);

            yield return StartCoroutine(FadeTextToZeroAlpha(1f, text));
            yield return new WaitForSeconds(0.5f);
        }
    }


    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
