using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public List<string[]> cluesCollected;
    public List<string> clueID;
    public List<string[]> itemMasterList;
    public int finalScores;
    public bool homoOrNah;
    bool fading;
    [SerializeField] Image blackScreen;
    public void goToContradiction(clueManager cM)
    {
        cluesCollected = new List<string[]>();
        clueID = new List<string>();
        cluesCollected = cM.cluesWritten;
        clueID = cM.itemsInteracted;
        StartCoroutine(fadeInBlack(-1));
        SceneManager.LoadScene(2);
        foreach(var clue in cluesCollected[0]) Debug.Log(clue.ToString());
        foreach(var items in clueID) Debug.Log(items.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {

        
        if(GameObject.FindGameObjectsWithTag("gameManager").Length > 1)
        {
            Destroy(gameObject);
        }
        //blackScreen.gameObject.SetActive(false);
        //StartCoroutine(fadeInBlack(-1));
    }
    private void Awake()
    {
        fading = false;
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
    public IEnumerator fadeInBlack(int multiplier)
    {
        blackScreen.gameObject.SetActive(true);
        if(multiplier >= 0 && !fading)
        {
            fading=true;
            float tmp = 0f;
            while (tmp <= 1)
            {
                blackScreen.color = new Color(0f, 0f, 0f, tmp);
                tmp += 0.1f * multiplier;
                Debug.Log(tmp);
                yield return new WaitForSeconds(0.05f);

            }
            blackScreen.color = new Color(0f, 0f, 0f, 1);
            yield return new WaitForSeconds(1f);
            yield return fadeInBlack(-1);
            //yield return null;
        }
        else
        {
  
            float tmp = 1f;
            while (tmp > 0)
            {
                blackScreen.color = new Color(0f, 0f, 0f, tmp);
                tmp += 0.1f * multiplier;
                yield return new WaitForSeconds(0.05f);

            }
            blackScreen.color = new Color(0f, 0f, 0f, 0f);
            blackScreen.gameObject.SetActive(false);
            fading= false;
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
