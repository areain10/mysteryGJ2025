using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public List<string[]> cluesCollected;
    public List<string> clueID;
    public List<string[]> itemMasterList;
    public void goToContradiction(clueManager cM)
    {
        cluesCollected = cM.cluesWritten;
        clueID = cM.itemsInteracted;
        SceneManager.LoadScene(1);
        foreach(var clue in cluesCollected[0]) Debug.Log(clue.ToString());
        foreach(var items in clueID) Debug.Log(items.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        loadItemCSV();
    }
    public void loadItemCSV()
    {

        itemMasterList = new List<string[]>();
        itemMasterList.Clear();
        var dataset = Resources.Load<TextAsset>("items");
        var dataLines = dataset.text.Split('\n');

        for (int i = 1; i < dataLines.Length; i++)
        {
            var data= dataLines[i].Split(';');
            //Debug.Log(data[1]);
            itemMasterList.Add(data);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
