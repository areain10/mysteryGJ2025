using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class importantButtons : MonoBehaviour
{
    importantItems iiManager;
    public string ItemID;

    // Start is called before the first frame update
    void Start()
    {
        //ItemID = Random.Range(0,100).ToString();
        iiManager = GameObject.FindAnyObjectByType<importantItems>();
        GetComponent<Image>().sprite = Resources.Load<Sprite>("button");
    }
    public void instantiationOfButton(string id)
    {
        ItemID = id;
        
        gameManager gm = GameObject.FindAnyObjectByType<gameManager>();
        foreach (var item in gm.itemMasterList)
        {
            //Debug.Log(gm.itemMasterList[0][0] + " " + itemID);
            if (item[0] == ItemID)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = item[1];

            }
        }
        
    }
    public void instantiationCaseButton(string id)
    {
        ItemID = id;

        gameManager gm = GameObject.FindAnyObjectByType<gameManager>();
        foreach (var item in gm.itemMasterList)
        {
            //Debug.Log(gm.itemMasterList[0][0] + " " + itemID);
            if (item[0] == ItemID)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = item[1];

            }
        }

    }
    public void toggleImportantItem()
    {
        Debug.Log("Hey this works");
        if (iiManager.importantItemsFinalList.Contains(ItemID))
        {
            iiManager.importantItemsFinalList.Remove(ItemID);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("button");

        }
        else
        {
            if(iiManager.importantItemsFinalList.Count < 7)
            {
                iiManager.importantItemsFinalList.Add(ItemID);
                GetComponent<Image>().sprite = Resources.Load<Sprite>("clicked");
            }
            
        }
    }
    public void selectReason()
    {
        casefilewriter writer = GameObject.FindAnyObjectByType<casefilewriter>();
        writer.currentSelectedEvidence = ItemID;

        casefileButtons[] tmp = FindObjectsByType<casefileButtons>(FindObjectsSortMode.None);
        foreach (var buttons in tmp)
        {
            buttons.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("button");

        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("clicked");


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
