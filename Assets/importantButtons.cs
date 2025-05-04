using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class importantButtons : MonoBehaviour
{
    importantItems iiManager;
    public string ItemID;
    bool pressed;
    // Start is called before the first frame update
    private void Awake()
    {
        iiManager = FindAnyObjectByType<importantItems>();
    }
    void Start()
    {
        //ItemID = Random.Range(0,100).ToString();
        iiManager = FindAnyObjectByType<importantItems>();
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
        foreach (var item in FindAnyObjectByType<importantItems>().importantItemsFinalList)
        {
                
            if (item == ItemID)
            {
                Debug.Log(gm.itemMasterList[0][0] + " " + ItemID);
                GetComponent<Image>().sprite = Resources.Load<Sprite>("clicked");
                pressed = true;

            }
        }

        
        //Debug.Log(ItemID);
        

        

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
        GetComponent<AudioSource>().Play();
        Debug.Log("Hey this works");
        if (iiManager.importantItemsFinalList.Contains(ItemID))
        {
            pressed = false;
            iiManager.importantItemsFinalList.Remove(ItemID);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("button");

        }
        else
        {
            if(iiManager.importantItemsFinalList.Count < 8)
            {
                pressed = true;
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
            if(buttons.gameObject != gameObject)
            {
                buttons.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("button");
                buttons.GetComponent<importantButtons>().pressed = false;

            }

        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("clicked");
        pressed = true;


    }
    // Update is called once per frame
    void Update()
    {
        if(pressed)
        {
            gameObject.GetComponent<Image>().color = Color.white;
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("circle");
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            gameObject.GetComponent<Image>().sprite = null;
        }
    }
}
