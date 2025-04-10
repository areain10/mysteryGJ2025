using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public List<string[]> cluesCollected;
    public void goToContradiction(clueManager cM)
    {
        cluesCollected = cM.cluesWritten;
        SceneManager.LoadScene(1);
        foreach(var clue in cluesCollected[0]) Debug.Log(clue.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
