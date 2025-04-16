using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class casefilewriter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI caseTextBox;
    [SerializeField] TextMeshProUGUI optionsTextBox;
    [SerializeField] List<string[]> writing;
    public List<string> importantID;
    int currentwriting;
    bool canchoose;
    int numOfOptions;
    string[] currentOption;
    // Start is called before the first frame update
    void Start()
    {
        /*writing = new List<string[]>
        {
            new string[] { "000 O’Brien",
            "000 not found", "001 found deceased"},
            new string[] { "001 ,Cause of death declared to be",
            "000 due to natural causes", "002 by suicide", "002 accidental" }
        };*/
        readCaseFileCSV();
        importantID = FindAnyObjectByType<gameManager>().clueID;
        currentwriting = -1;
        canchoose = false;
        caseTextBox.text = "";
        loadNextLine(1);
       
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
    void loadNextLine(int lineNumber)
    {
        currentwriting = lineNumber;
        caseTextBox.text += writing[currentwriting][2]+" ";
        
        displayOptions(writing[currentwriting][3].Split('|'));
    }
    void displayOptions(string[] currentOptions)
    {
        currentOption = currentOptions;
        numOfOptions = 0;
        canchoose = true;
        optionsTextBox.text = "";
        for(int i=0; i < currentOptions.Length; i++)
        {
            Debug.Log(Int32.Parse(currentOptions[i]));
            if (checkIfShouldDisplay(writing[Int32.Parse(currentOptions[i])]))
            {
                numOfOptions++;
                optionsTextBox.text += (numOfOptions).ToString() + "." + writing[Int32.Parse(currentOptions[i])][2] + "\n";
            }
            
        }
    }
    bool checkIfShouldDisplay(string[] line)
    {
        bool tmp = false;
        string[] listOfNeeded = line[1].Split('|');
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
    void chooseOption(int option)
    {
        loadNextLine(Int32.Parse(currentOption[option-1]));
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
                    chooseOption(i);
                }
            }
        }
    }
}
