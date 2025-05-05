using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<string> lines;

    void Start()
    {
        StartCoroutine(PlayPrologue());
    }

    private IEnumerator Typing(int num)
    {
        text.text = ""; 
        string currentWriting = lines[num];

        int count = 0;

        foreach (char c in currentWriting)
        {
            if (count % 3 == 0)
             {
                 GetComponent<AudioSource>().Play();
             }

             count++;
            text.text += c;
            yield return new WaitForSeconds(0.04f);
        }
    }

    private void Continue()
    {
        SceneManager.LoadScene(2);
    }

    private IEnumerator Line(int line) 
    {
        yield return StartCoroutine(Typing(line));
        yield return new WaitForSeconds(2); 
    }

    private IEnumerator PlayPrologue()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            yield return StartCoroutine(Line(i));
        }

        Continue();
    }
}
