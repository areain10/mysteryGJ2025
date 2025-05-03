using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class casefilewriter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI caseTextBox;
    [SerializeField] TextMeshProUGUI optionsTextBox;
    [SerializeField] List<string[]> writing;
    [SerializeField] GameObject ContainerGO;
    [SerializeField] GameObject ItemButtonPrefab;
    [SerializeField] GameObject OptionsButtonPrefab;
    public List<string> importantID;
    int currentwriting;
    bool canchoose;
    int numOfOptions;
    string[] currentOption;
    public string currentSelectedEvidence;
    string[] listOfNeeded;
    Color textColor;
    int pressedOption;
    int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        finalScore = 0;
        /*writing = new List<string[]>
        {
            new string[] { "000 O’Brien",
            "000 not found", "001 found deceased"},
            new string[] { "001 ,Cause of death declared to be",
            "000 due to natural causes", "002 by suicide", "002 accidental" }
        };*/
        readCaseFileCSV();
        //importantID = FindAnyObjectByType<gameManager>().clueID;
        currentwriting = -1;
        canchoose = false;
        caseTextBox.text = "";
        currentSelectedEvidence = "";
        pressedOption = -1;
        optionsTextBox.text = "";
        optionsTextBox.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < importantID.Count; i++)
        {
            GameObject go = Instantiate(ItemButtonPrefab);
            go.GetComponent<importantButtons>().instantiationCaseButton(importantID[i]);
            //go.GetComponentInChildren<TextMeshProUGUI>().fontSize = 18;
            //go.transform.localScale = new Vector3(1,0.3f);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - (30 * i), go.transform.position.z);
            go.transform.SetParent(ContainerGO.transform, false);
        }
        StartCoroutine(loadNextLine(1, false));
        
       
    }
    public void readCaseFileCSV()
    {
        writing = new List<string[]>();
        writing.Clear();
        var dataset = Resources.Load<TextAsset>("casefile");
        string[] dataLines = dataset.text.Split('\n');

        for (int i = 0; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(';');
           
            writing.Add(data);
        
        }
    }
 
    IEnumerator loadNextLine(int lineNumber,bool includeClue)
    {

        optionsTextBox.transform.parent.gameObject.SetActive(false);
        currentwriting = lineNumber;
        string tmp = writing[currentwriting][2].Replace("\\n", "\n") + " ";
        canchoose = false;
        foreach(char c in tmp)
        {
            caseTextBox.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        if(includeClue)
        {

            string clue = GameObject.FindAnyObjectByType<gameManager>().itemMasterList[Int32.Parse(currentSelectedEvidence)-1][1];
            caseTextBox.text += "(";
            foreach (char c in clue)
            {
                caseTextBox.text += c;
                yield return new WaitForSeconds(0.001f);
            }
            caseTextBox.text += ")";
            currentSelectedEvidence = "";
            
            pressedOption = -1;
        }
        casefileButtons[] tmps = FindObjectsByType<casefileButtons>(FindObjectsSortMode.None);
        foreach (var buttons in tmps)
        {
            buttons.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("button");

        }
        displayOptions(writing[currentwriting][3].Split('|'));
       
    }

    void displayOptions(string[] currentOptions)
    {
        foreach (var x in optionsTextBox.transform.parent.GetComponentsInChildren<optionsButton>())
        {
            Destroy(x.gameObject);
        }
        textColor = Color.black;
        optionsTextBox.transform.parent.gameObject.SetActive(true);
        currentOption = currentOptions;
        numOfOptions = 0;
        canchoose = true;
        optionsTextBox.text = "";
        for(int i=0; i < currentOptions.Length; i++)
        {
            //Debug.Log(Int32.Parse(currentOptions[i]));
            try
            {
                
                if (checkIfShouldDisplay(writing[Int32.Parse(currentOptions[i])]))
                {
                    numOfOptions++;
                    optionsTextBox.color = Color.black;
                    string tmp;
                    Debug.Log(writing[Int32.Parse(currentOptions[i])][1].Split('|')[0] + ":" + writing[Int32.Parse(currentOptions[i])][1].Split('|')[0] + "  " + (writing[Int32.Parse(currentOptions[i])][1].Split('|')[0] != "000"));
                    if (writing[Int32.Parse(currentOptions[i])][1].Split('|')[0] != "000")
                    {
                        
                        tmp = "<color=yellow>"+(numOfOptions).ToString() + "." + writing[Int32.Parse(currentOptions[i])][2].Replace("\\n", "") + "<color=yellow>" +"\n";
                    }
                    else
                    {
                        tmp= (numOfOptions).ToString() + "." + writing[Int32.Parse(currentOptions[i])][2].Replace("\\n", "") + "\n";

                    }
                    GameObject go = Instantiate(OptionsButtonPrefab,optionsTextBox.transform.parent);
                    //GameObject go = Instantiate(ItemButtonPrefab);
                    go.GetComponentInChildren<TextMeshProUGUI>().text = tmp;
                    go.GetComponentInChildren<optionsButton>().num = numOfOptions;
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - (100 * (i-1)), go.transform.position.z);
                    //go.transform.SetParent(ContainerGO.transform, false);
                    //optionsTextBox.color = Color.black;
                }
            }
            catch
            {
                Debug.Log("Game End\n Final SCore:"+finalScore);
            }
            
            
        }
    }
    bool checkIfShouldDisplay(string[] line)
    {
        
        bool tmp = false;
        listOfNeeded = line[1].Split('|');
        foreach(var c in listOfNeeded)
        {
            if (c == "000")
            {
                
                tmp = true;
            }
            else
            {
                foreach(var x in importantID)
                {
                    if (c == x)
                    {
                        tmp = true;
                        
                    }
                }
            }
        }
        return tmp;
    }
    public void chooseOption(int option)
    {
        pressedOption = option;
        checkIfShouldDisplay(writing[Int32.Parse(currentOption[option - 1])]);
        
        string[] tmp = writing[Int32.Parse(currentOption[option - 1])][1].Split('|');
        foreach (var c in tmp)
        {
            //Debug.Log(c);
        }
        Debug.Log(writing[Int32.Parse(currentOption[option - 1])][1].Split('|')[0]);
        if (Int32.Parse(writing[Int32.Parse(currentOption[option - 1])][1].Split('|')[0]) == 0 || writing[Int32.Parse(currentOption[option - 1])][1].Split('|').Length != 1)
        {
            StartCoroutine(loadNextLine(Int32.Parse(currentOption[option - 1]),false));
        }
        else if (currentSelectedEvidence!="")
        {
            foreach(var x in writing[Int32.Parse(currentOption[option - 1])][1].Split('|'))
            {
                Debug.Log(x+"            "+currentSelectedEvidence);
            }
            if (writing[Int32.Parse(currentOption[option - 1])][1].Split('|').ToList().Contains(currentSelectedEvidence))
            {
                Debug.Log("YOU GOT IT RIGHT");
                finalScore += 1;
            }
            StartCoroutine(loadNextLine(Int32.Parse(currentOption[option - 1]),true));
        }
        
        //caseTextBox.text += " "+ writing[currentwriting][option];
        //loadNextLine();
    }
    // Update is called once per frame
    void Update()
    {
        if(canchoose)
        {
            for (int i = 1; i <= numOfOptions; i++)
            {
                if (Input.GetKeyDown((KeyCode)(48 + i)))
                {
                    //chooseOption(i);
                }
            }
        }
    }
    
}
